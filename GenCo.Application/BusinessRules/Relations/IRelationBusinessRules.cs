using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Relations;

public interface IRelationBusinessRules
{
    Task EnsureRelationTypeValidAsync(string type);
    Task EnsureDeleteBehaviorValidAsync(string behavior);
    Task EnsureEntitiesExistAsync(Guid fromEntityId, Guid toEntityId, CancellationToken cancellationToken);
    Task EnsureNoCircularRelationAsync(Guid fromEntityId, Guid toEntityId);
    Task EnsureRelationUniqueOnCreateAsync(
        Guid projectId,
        Guid fromEntityId,
        Guid toEntityId,
        string type,
        CancellationToken cancellationToken);
    Task EnsureRelationUniqueOnUpdateAsync(
        Guid projectId,
        Guid fromEntityId,
        Guid toEntityId,
        Guid relationId,
        string type,
        CancellationToken cancellationToken);
    Task EnsureJoinTableConsistencyAsync(Relation relation);
    Task EnsureFieldMappingConsistencyAsync(Relation relation);
    Task EnsureDeleteBehaviorCompatibilityAsync(Relation relation);
    Task EnsureRelationNameValidAsync(string? name);
}