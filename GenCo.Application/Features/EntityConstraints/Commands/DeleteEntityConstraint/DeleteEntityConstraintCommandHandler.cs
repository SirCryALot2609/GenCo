using GenCo.Application.BusinessRules.EntityConstraints;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.DeleteEntityConstraint;

public class DeleteEntityConstraintCommandHandler(
    IGenericRepository<EntityConstraint> repository,
    IUnitOfWork unitOfWork,
    IEntityConstraintBusinessRules businessRules)
    : IRequestHandler<DeleteEntityConstraintCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteEntityConstraintCommand request, CancellationToken cancellationToken)
    {
        var constraint = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (constraint == null)
            return BaseResponseDto<bool>.Fail("EntityConstraint not found");

        // 🧩 Business validation
        await businessRules.EnsureConstraintCanBeDeletedAsync(request.Id, cancellationToken);

        await repository.DeleteAsync(constraint, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraint deleted successfully");
    }
}