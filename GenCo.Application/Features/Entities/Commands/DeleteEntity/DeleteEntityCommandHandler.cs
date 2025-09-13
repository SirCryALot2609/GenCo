using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.DeleteEntity;

public class DeleteEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IEntityBusinessRules rules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEntityCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
    {
        // ✅ Validate
        await rules.EnsureEntityCanBeDeletedAsync(request.Id, cancellationToken);

        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        await repository.DeleteAsync(entity!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Entity deleted successfully");
    }
}
