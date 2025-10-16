using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Fields;

public interface IFieldBusinessRules
{
    Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken);
    Task EnsureFieldBelongsToEntityAsync(Guid fieldId, Guid entityId, CancellationToken cancellationToken);
    Task EnsureFieldNameUniqueOnCreateAsync(Guid entityId, string columnName, CancellationToken cancellationToken);
    Task EnsureFieldNameUniqueOnUpdateAsync(Guid entityId, Guid fieldId, string columnName, CancellationToken cancellationToken);
    Task EnsureFieldCanBeDeletedAsync(Guid fieldId, CancellationToken cancellationToken);
    Task EnsureFieldTypeValidAsync(string type);
    Task EnsureFieldConfigurationValidAsync(Field field);
    Task EnsureValidatorsValidAsync(Field field);
}