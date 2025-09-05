using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.RestoreField;

public class RestoreFieldCommandHandler(
    IGenericRepository<Field> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (field == null)
            return BaseResponseDto<bool>.Fail("Field not found");

        await repository.RestoreAsync(field, cancellationToken);
        field.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Field restored successfully");
    }
}