using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.RestoreEntityConstraintField;

public class RestoreEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreEntityConstraintFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("EntityConstraintField not found");

        await repository.RestoreAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraintField restored successfully");
    }
}