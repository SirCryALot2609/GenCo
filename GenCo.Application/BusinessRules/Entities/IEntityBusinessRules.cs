namespace GenCo.Application.BusinessRules.Entities;

public interface IEntityBusinessRules
{
    // Project & Entity existence
    Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken);
    Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureEntityBelongsToProjectAsync(Guid entityId, Guid projectId, CancellationToken cancellationToken);

    // Name uniqueness
    Task EnsureEntityNameUniqueOnCreateAsync(Guid projectId, string name, CancellationToken cancellationToken);
    Task EnsureEntityNameUniqueOnUpdateAsync(Guid projectId, Guid entityId, string name, CancellationToken cancellationToken);

    // Delete safety
    Task EnsureEntityCanBeDeletedAsync(Guid entityId, CancellationToken cancellationToken);

    // Field & Constraints
    Task EnsureFieldNameUniqueAsync(Guid entityId, string fieldName, CancellationToken cancellationToken);
    Task EnsureConstraintsValidAsync(Guid entityId, CancellationToken cancellationToken);

    // Optional naming convention
    Task EnsureEntityNameFollowsConventionAsync(string name);
}
