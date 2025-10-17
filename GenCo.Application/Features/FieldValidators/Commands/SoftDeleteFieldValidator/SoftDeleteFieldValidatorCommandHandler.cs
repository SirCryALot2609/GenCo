using GenCo.Application.BusinessRules.FieldValidators;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.SoftDeleteFieldValidator;
public sealed class SoftDeleteFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> validatorRepository,
    IFieldValidatorBusinessRules businessRules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteFieldValidatorCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteFieldValidatorCommand request, CancellationToken cancellationToken)
    {
        await businessRules.EnsureValidatorExistsAsync(request.Id, cancellationToken);
        await businessRules.EnsureCanDeleteAsync(request.Id, cancellationToken);

        var validator = await validatorRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<bool>.Fail("FieldValidator not found");

        await validatorRepository.SoftDeleteAsync(validator, cancellationToken);
        validator.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "FieldValidator soft deleted successfully");
    }
}

