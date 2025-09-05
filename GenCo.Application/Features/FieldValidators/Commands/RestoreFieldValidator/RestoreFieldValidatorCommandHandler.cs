using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.RestoreFieldValidator;
public class RestoreFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreFieldValidatorCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreFieldValidatorCommand request, CancellationToken cancellationToken)
    {
        var validator = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<bool>.Fail("FieldValidator not found");

        await repository.RestoreAsync(validator, cancellationToken);
        validator.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "FieldValidator restored successfully");
    }
}
