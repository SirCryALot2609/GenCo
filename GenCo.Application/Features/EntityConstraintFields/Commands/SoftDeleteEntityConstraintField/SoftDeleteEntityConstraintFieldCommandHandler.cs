using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.SoftDeleteEntityConstraintField;

public class SoftDeleteEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteEntityConstraintFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("EntityConstraintField not found");

        await repository.SoftDeleteAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraintField soft deleted successfully");
    }
}