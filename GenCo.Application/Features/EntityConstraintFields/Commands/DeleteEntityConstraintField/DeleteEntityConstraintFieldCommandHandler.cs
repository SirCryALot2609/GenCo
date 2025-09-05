using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.DeleteEntityConstraintField;

public class DeleteEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEntityConstraintFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("EntityConstraintField not found");

        await repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraintField deleted successfully");
    }
}