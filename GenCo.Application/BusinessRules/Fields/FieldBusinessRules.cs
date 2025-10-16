using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.Fields;
using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Fields;

public class FieldBusinessRules(
    IGenericRepository<Field> fieldRepository,
    IGenericRepository<Entity> entityRepository)
    : IFieldBusinessRules
{
    private static readonly string[] ValidFieldTypes = 
        ["string", "int", "decimal", "datetime", "bool", "guid"];

    public async Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        var exists = await entityRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");
    }

    public async Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(fieldId);
        var exists = await fieldRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");
    }

    public async Task EnsureFieldBelongsToEntityAsync(Guid fieldId, Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(fieldId);
        var field = await fieldRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (field is null)
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");

        if (field.EntityId != entityId)
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not belong to Entity {entityId}.",
                "FIELD_ENTITY_MISMATCH");
    }

    public async Task EnsureFieldNameUniqueOnCreateAsync(Guid entityId, string columnName, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(columnName);

        var spec = new FieldByEntityAndNameSpec(entityId, columnName);
        var exists = await fieldRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Field with name '{columnName}' already exists in entity {entityId}.",
                "FIELD_NAME_DUPLICATED");
    }

    public async Task EnsureFieldNameUniqueOnUpdateAsync(Guid entityId, Guid fieldId, string columnName, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(columnName);

        var spec = new FieldByEntityAndNameSpec(entityId, columnName, excludeFieldId: fieldId);
        var exists = await fieldRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Field with name '{columnName}' already exists in entity {entityId}.",
                "FIELD_NAME_DUPLICATED");
    }

    public async Task EnsureFieldCanBeDeletedAsync(Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(fieldId, includeValidators: true);
        var field = await fieldRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (field is null)
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");

        if (field.Validators is { Count: > 0 })
            throw new BusinessRuleValidationException(
                "Cannot delete field that still has validators.",
                "FIELD_HAS_VALIDATORS");
    }

    public Task EnsureFieldTypeValidAsync(string type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type);

        if (!ValidFieldTypes.Contains(type, StringComparer.OrdinalIgnoreCase))
            throw new BusinessRuleValidationException(
                $"Invalid field type '{type}'. Allowed types: {string.Join(", ", ValidFieldTypes)}.",
                "FIELD_TYPE_INVALID");

        return Task.CompletedTask;
    }

    public async Task EnsureFieldConfigurationValidAsync(Field field)
    {
        ArgumentNullException.ThrowIfNull(field);

        await EnsureFieldTypeValidAsync(field.Type).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(field.ColumnName))
            throw new BusinessRuleValidationException("Field column name is required.", "FIELD_NAME_REQUIRED");

        if (field.Length <= 0)
            throw new BusinessRuleValidationException("Field length must be greater than zero.", "FIELD_LENGTH_INVALID");

        if (field.Scale < 0)
            throw new BusinessRuleValidationException("Field scale must be non-negative.", "FIELD_SCALE_INVALID");

        if (field.IsAutoIncrement)
        {
            if (!field.Type.Equals("int", StringComparison.OrdinalIgnoreCase))
                throw new BusinessRuleValidationException("Auto increment is only allowed for 'int' type.", "FIELD_AUTOINC_TYPE_INVALID");

            if (!string.IsNullOrEmpty(field.DefaultValue))
                throw new BusinessRuleValidationException("Auto increment field cannot have a default value.", "FIELD_AUTOINC_DEFAULT_CONFLICT");
        }

        if (field.IsRequired && string.IsNullOrWhiteSpace(field.DefaultValue) && !field.IsAutoIncrement)
            throw new BusinessRuleValidationException("Required fields should have a default value or be auto increment.", "FIELD_REQUIRED_INVALID");

        if (field.ColumnOrder < 0)
            throw new BusinessRuleValidationException("Column order must be non-negative.", "FIELD_ORDER_INVALID");
    }

    public Task EnsureValidatorsValidAsync(Field field)
    {
        ArgumentNullException.ThrowIfNull(field);

        if (field.Validators is null or { Count: 0 })
            return Task.CompletedTask;

        foreach (var validator in field.Validators)
        {
            if (validator is null)
                throw new BusinessRuleValidationException(
                    $"Null validator detected for field '{field.ColumnName}'.",
                    "FIELD_VALIDATOR_NULL");

            if (validator.ConfigObject == null && !string.IsNullOrWhiteSpace(validator.ConfigJson))
                throw new BusinessRuleValidationException(
                    $"Invalid validator configuration for field '{field.ColumnName}'.",
                    "FIELD_VALIDATOR_CONFIG_INVALID");
        }

        return Task.CompletedTask;
    }
}
