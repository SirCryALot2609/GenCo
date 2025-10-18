using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.RelationFieldMappings;

public interface IRelationFieldMappingBusinessRules
{
    Task EnsureRelationExistsAsync(Guid relationId, CancellationToken cancellationToken);
    Task EnsureFieldsExistAsync(Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken);
    Task EnsureFieldsBelongToCorrectEntitiesAsync(Guid relationId, Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken);
    Task EnsureNoDuplicateMappingAsync(Guid relationId, Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken);
    Task EnsureFieldTypesCompatibleAsync(Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken);
    Task EnsureMappingCanBeDeletedAsync(Guid mappingId, CancellationToken cancellationToken);
}
