using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.EntityConstraintFields;
using GenCo.Application.Specifications.EntityConstraints;
using GenCo.Application.Specifications.Fields;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.EntityConstraintFields;

public class EntityConstraintFieldBusinessRules(
    IGenericRepository<EntityConstraint> constraintRepository,
    IGenericRepository<Field> fieldRepository,
    IGenericRepository<EntityConstraintField> constraintFieldRepository)
    : IEntityConstraintFieldBusinessRules
{
    // ================== EXISTENCE CHECKS ==================

    public async Task EnsureConstraintExistsAsync(Guid constraintId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(constraintId);
        if (!await constraintRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Constraint with Id {constraintId} does not exist.",
                "CONSTRAINT_NOT_FOUND");
    }

    public async Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(fieldId);
        if (!await fieldRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");
    }

    // ================== FIELD CONSTRAINT RELATIONSHIP VALIDATION ==================

    public async Task EnsureConstraintAllowsFieldsAsync(Guid constraintId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(constraintId);
        var constraint = await constraintRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (constraint is null)
            throw new BusinessRuleValidationException(
                $"Constraint with Id {constraintId} not found.",
                "CONSTRAINT_NOT_FOUND");

        if (constraint.Type == ConstraintType.Check)
            throw new BusinessRuleValidationException(
                "Check constraints cannot have fields assigned.",
                "CONSTRAINT_TYPE_NOT_SUPPORT_FIELDS");
    }

    public async Task EnsureFieldBelongsToEntityAsync(Guid constraintId, Guid fieldId, CancellationToken cancellationToken)
    {
        var constraintSpec = new EntityConstraintByIdSpec(constraintId, includeDetails: true);
        var constraint = await constraintRepository.FirstOrDefaultAsync(constraintSpec, cancellationToken: cancellationToken);

        if (constraint is null)
            throw new BusinessRuleValidationException(
                $"Constraint with Id {constraintId} does not exist.",
                "CONSTRAINT_NOT_FOUND");

        var fieldSpec = new FieldByIdSpec(fieldId);
        var field = await fieldRepository.FirstOrDefaultAsync(fieldSpec, cancellationToken: cancellationToken);

        if (field is null)
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");

        if (field.EntityId != constraint.EntityId)
            throw new BusinessRuleValidationException(
                $"Field {fieldId} does not belong to Entity {constraint.EntityId}.",
                "FIELD_ENTITY_MISMATCH");
    }

    public async Task EnsureFieldNotDuplicatedOnCreateAsync(Guid constraintId, Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintFieldByConstraintAndFieldSpec(constraintId, fieldId);
        if (await constraintFieldRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Field {fieldId} already exists in constraint {constraintId}.",
                "FIELD_DUPLICATED_IN_CONSTRAINT");
    }
    
    
    public async Task EnsureFieldNotDuplicatedOnUpdateAsync(
        Guid constraintId,
        Guid fieldId,
        Guid constraintFieldId,
        CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintFieldByConstraintAndFieldSpec(constraintId, fieldId);
        var existing = await constraintFieldRepository.FirstOrDefaultAsync(spec, cancellationToken : cancellationToken);

        // Nếu đã tồn tại 1 bản ghi khác với cùng ConstraintId & FieldId => trùng lặp
        if (existing != null && existing.Id != constraintFieldId)
            throw new BusinessRuleValidationException(
                $"Field {fieldId} already exists in constraint {constraintId}.",
                "FIELD_DUPLICATED_IN_CONSTRAINT");
    }


    // ================== DELETE SAFETY ==================

    public async Task EnsureCanDeleteAsync(Guid constraintFieldId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintFieldByIdSpec(constraintFieldId, includeDetails: true);
        var constraintField = await constraintFieldRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (constraintField is null)
            throw new BusinessRuleValidationException(
                $"Constraint field with Id {constraintFieldId} not found.",
                "CONSTRAINT_FIELD_NOT_FOUND");

        var constraint = constraintField.Constraint;

        // Nếu là PrimaryKey và chỉ còn 1 field -> không cho xóa
        if (constraint.Type == ConstraintType.PrimaryKey && constraint.Fields.Count <= 1)
            throw new BusinessRuleValidationException(
                "Cannot remove the last field from a primary key constraint.",
                "PK_LAST_FIELD_CANNOT_BE_REMOVED");
    }
}