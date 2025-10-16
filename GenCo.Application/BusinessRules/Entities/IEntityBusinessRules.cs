namespace GenCo.Application.BusinessRules.Entities;

public interface IEntityBusinessRules
{
    Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken);
    Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureEntityBelongsToProjectAsync(Guid entityId, Guid projectId, CancellationToken cancellationToken);
    Task EnsureEntityNameUniqueOnCreateAsync(Guid projectId, string name, CancellationToken cancellationToken);
    Task EnsureEntityNameUniqueOnUpdateAsync(Guid projectId, Guid entityId, string name, CancellationToken cancellationToken);
    Task EnsureEntityCanBeDeletedAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureFieldNameUniqueAsync(Guid entityId, string fieldName, CancellationToken cancellationToken);
    Task EnsureConstraintsValidAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureEntityNameFollowsConventionAsync(string name);
}
