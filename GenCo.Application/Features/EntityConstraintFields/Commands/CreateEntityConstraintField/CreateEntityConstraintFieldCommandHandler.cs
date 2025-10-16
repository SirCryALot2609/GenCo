using AutoMapper;
using GenCo.Application.BusinessRules.EntityConstraintFields;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.CreateEntityConstraintField;

public class CreateEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IEntityConstraintFieldBusinessRules rules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityConstraintFieldCommand, BaseResponseDto<EntityConstraintFieldResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintFieldResponseDto>> Handle(CreateEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        // ==== Business validation ====
        await rules.EnsureConstraintExistsAsync(dto.ConstraintId, cancellationToken);
        await rules.EnsureFieldExistsAsync(dto.FieldId, cancellationToken);
        await rules.EnsureFieldBelongsToEntityAsync(dto.ConstraintId, dto.FieldId, cancellationToken);
        await rules.EnsureFieldNotDuplicatedOnCreateAsync(dto.ConstraintId, dto.FieldId, cancellationToken);

        var entity = mapper.Map<EntityConstraintField>(dto);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EntityConstraintFieldResponseDto>(entity);
        return BaseResponseDto<EntityConstraintFieldResponseDto>.Ok(response, "EntityConstraintField created successfully");
    }
}
