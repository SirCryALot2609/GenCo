using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.EntityConstraints;

public interface IEntityConstraintBusinessRules
{
    Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken);
    Task EnsureConstraintExistsAsync(Guid constraintId, CancellationToken cancellationToken);
    Task EnsureConstraintBelongsToEntityAsync(Guid constraintId, Guid entityId, CancellationToken cancellationToken);
    Task EnsureConstraintNameUniqueOnCreateAsync(Guid entityId, string? constraintName, CancellationToken cancellationToken);
    Task EnsureConstraintNameUniqueOnUpdateAsync(Guid entityId, Guid constraintId, string? constraintName, CancellationToken cancellationToken);

    Task EnsureConstraintCanBeDeletedAsync(Guid constraintId, CancellationToken cancellationToken);

    Task EnsureConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);

    Task EnsureConstraintNameFollowsConventionAsync(string? name);
    Task EnsurePrimaryKeyValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);
    Task EnsureUniqueConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);
    Task EnsureIndexConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);
    Task EnsureCheckConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);
    Task EnsureForeignKeyValidAsync(EntityConstraint constraint, CancellationToken cancellationToken);
}