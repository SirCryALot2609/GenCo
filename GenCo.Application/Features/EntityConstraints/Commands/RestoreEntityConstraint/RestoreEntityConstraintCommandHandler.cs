using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.RestoreEntityConstraint;


public class RestoreEntityConstraintCommandHandler(
    IGenericRepository<EntityConstraint> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreEntityConstraintCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreEntityConstraintCommand request, CancellationToken cancellationToken)
    {
        var constraint = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (constraint == null)
            return BaseResponseDto<bool>.Fail("EntityConstraint not found");

        await repository.RestoreAsync(constraint, cancellationToken);
        constraint.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraint restored successfully");
    }
}