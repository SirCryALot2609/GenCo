using GenCo.Application.BusinessRules.EntityConstraintFields;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.DeleteEntityConstraintField;

public class DeleteEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IEntityConstraintFieldBusinessRules rules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEntityConstraintFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        await rules.EnsureFieldExistsAsync(request.Id, cancellationToken);
        await rules.EnsureCanDeleteAsync(request.Id, cancellationToken);

        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        await repository.DeleteAsync(entity!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "EntityConstraintField deleted successfully");
    }
}
