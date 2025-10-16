namespace GenCo.Application.BusinessRules.EntityConstraintFields;

public interface IEntityConstraintFieldBusinessRules
{
    Task EnsureConstraintExistsAsync(Guid constraintId, CancellationToken cancellationToken);
    Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken);
    Task EnsureConstraintAllowsFieldsAsync(Guid constraintId, CancellationToken cancellationToken);
    Task EnsureFieldBelongsToEntityAsync(Guid constraintId, Guid fieldId, CancellationToken cancellationToken);
    Task EnsureFieldNotDuplicatedOnCreateAsync(Guid constraintId, Guid fieldId, CancellationToken cancellationToken);
    Task EnsureFieldNotDuplicatedOnUpdateAsync(Guid constraintId, Guid fieldId, Guid constraintFieldId, CancellationToken cancellationToken);
    Task EnsureCanDeleteAsync(Guid constraintFieldId, CancellationToken cancellationToken);
}
