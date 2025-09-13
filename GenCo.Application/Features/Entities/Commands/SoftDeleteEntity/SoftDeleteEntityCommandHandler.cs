using AutoMapper;
using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.SoftDeleteEntity;

public class SoftDeleteEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IEntityBusinessRules rules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteEntityCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteEntityCommand request, CancellationToken cancellationToken)
    {
        await rules.EnsureEntityExistsAsync(request.Id, cancellationToken);

        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        await repository.SoftDeleteAsync(entity!, cancellationToken);

        entity!.UpdatedAt = DateTime.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Entity soft deleted successfully");
    }
}
