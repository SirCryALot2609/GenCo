using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.FieldValidators;

public interface IFieldValidatorBusinessRules
{
    Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken);
    Task EnsureValidatorExistsAsync(Guid validatorId, CancellationToken cancellationToken);
    Task EnsureValidatorBelongsToFieldAsync(Guid validatorId, Guid fieldId, CancellationToken cancellationToken);
    Task EnsureValidatorTypeValidAsync(string type);
    Task EnsureValidatorConfigValidAsync(FieldValidator validator);
    Task EnsureValidatorUniqueOnCreateAsync(Guid fieldId, string type, CancellationToken cancellationToken);
    Task EnsureValidatorUniqueOnUpdateAsync(Guid fieldId, Guid validatorId, string type, CancellationToken cancellationToken);
    Task EnsureCanDeleteAsync(Guid validatorId, CancellationToken cancellationToken);
}
