using AutoMapper;
using FluentValidation;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.CreateEntity;

public class CreateEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityCommand, BaseResponseDto<EntityResponseDto>>
{
    public async Task<BaseResponseDto<EntityResponseDto>> Handle(
        CreateEntityCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Entity>(request.Request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityResponseDto>(entity);
        return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity created successfully");
    }
}
