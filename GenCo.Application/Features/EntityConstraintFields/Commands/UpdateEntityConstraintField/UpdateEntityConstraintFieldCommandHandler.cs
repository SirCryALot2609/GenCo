using AutoMapper;
using GenCo.Application.BusinessRules.EntityConstraintFields;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.UpdateEntityConstraintField;

public class UpdateEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IEntityConstraintFieldBusinessRules rules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateEntityConstraintFieldCommand, BaseResponseDto<EntityConstraintFieldResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintFieldResponseDto>> Handle(UpdateEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        await rules.EnsureFieldExistsAsync(dto.Id, cancellationToken);
        await rules.EnsureConstraintExistsAsync(dto.ConstraintId, cancellationToken);
        await rules.EnsureFieldExistsAsync(dto.FieldId, cancellationToken);
        await rules.EnsureFieldBelongsToEntityAsync(dto.ConstraintId, dto.FieldId, cancellationToken);
        await rules.EnsureFieldNotDuplicatedOnUpdateAsync(dto.Id, dto.ConstraintId, dto.FieldId, cancellationToken);

        var entity = await repository.GetByIdAsync(dto.Id, cancellationToken: cancellationToken);
        mapper.Map(dto, entity);
        entity!.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EntityConstraintFieldResponseDto>(entity);
        return BaseResponseDto<EntityConstraintFieldResponseDto>.Ok(response, "EntityConstraintField updated successfully");
    }
}
