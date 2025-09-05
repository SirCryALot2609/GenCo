using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.DeleteField;

public class DeleteFieldCommandHandler(
    IGenericRepository<Field> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (field == null)
            return BaseResponseDto<bool>.Fail("Field not found");

        await repository.DeleteAsync(field, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Field deleted successfully");
    }
}