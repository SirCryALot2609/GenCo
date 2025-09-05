using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.CreateEntityConstraintField;

public class CreateEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityConstraintFieldCommand, BaseResponseDto<EntityConstraintFieldResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintFieldResponseDto>> Handle(CreateEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<EntityConstraintField>(request.Request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityConstraintFieldResponseDto>(entity);
        return BaseResponseDto<EntityConstraintFieldResponseDto>.Ok(dto, "EntityConstraintField created successfully");
    }
}