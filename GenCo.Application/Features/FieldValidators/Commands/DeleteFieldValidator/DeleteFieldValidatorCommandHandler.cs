using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator;
public class DeleteFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFieldValidatorCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteFieldValidatorCommand request, CancellationToken cancellationToken)
    {
        var validator = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<bool>.Fail("FieldValidator not found");

        await repository.DeleteAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "FieldValidator deleted successfully");
    }
}
