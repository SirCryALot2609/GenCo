using GenCo.Application.BusinessRules.FieldValidators;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator;
public sealed class DeleteFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> validatorRepository,
    IFieldValidatorBusinessRules businessRules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFieldValidatorCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteFieldValidatorCommand request, CancellationToken cancellationToken)
    {
        await businessRules.EnsureValidatorExistsAsync(request.Id, cancellationToken);
        await businessRules.EnsureCanDeleteAsync(request.Id, cancellationToken);

        var validator = await validatorRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<bool>.Fail("FieldValidator not found");

        await validatorRepository.DeleteAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "FieldValidator deleted successfully");
    }
}
