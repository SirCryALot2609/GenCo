using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
namespace GenCo.Application.Features.Entities.Commands.UpdateEntity;
public class UpdateEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateEntityCommand, BaseResponseDto<EntityResponseDto>>
{
    public async Task<BaseResponseDto<EntityResponseDto>> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<EntityResponseDto>.Fail("Entity not found");

        // Chỉ update field khác null
        mapper.Map(request.Request, entity);

        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityResponseDto>(entity);
        return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity updated successfully");
    }
}