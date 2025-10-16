using AutoMapper;
using FluentValidation;
using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.CreateEntity;

public class CreateEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IEntityBusinessRules rules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityCommand, BaseResponseDto<EntityResponseDto>>
{
    public async Task<BaseResponseDto<EntityResponseDto>> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        await rules.EnsureProjectExistsAsync(request.Request.ProjectId, cancellationToken);
        await rules.EnsureEntityNameUniqueOnCreateAsync(request.Request.ProjectId, request.Request.Name, cancellationToken);

        var entity = mapper.Map<Entity>(request.Request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;

        await repository.AddAsync(entity, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            throw new BusinessRuleValidationException("Failed to save entity", "SAVE_FAILED");

        var dto = mapper.Map<EntityResponseDto>(entity);
        return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity created successfully");
    }
}




