using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.RestoreEntity;

public class RestoreEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IEntityBusinessRules rules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreEntityCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreEntityCommand request, CancellationToken cancellationToken)
    {
        await rules.EnsureEntityExistsAsync(request.Id, cancellationToken);

        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        await repository.RestoreAsync(entity!, cancellationToken);

        entity!.UpdatedAt = DateTime.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Entity restored successfully");
    }
}
