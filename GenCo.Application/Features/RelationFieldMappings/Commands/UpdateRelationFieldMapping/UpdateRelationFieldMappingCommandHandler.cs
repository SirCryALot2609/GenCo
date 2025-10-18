using AutoMapper;
using GenCo.Application.BusinessRules.RelationFieldMappings;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.UpdateRelationFieldMapping;

public class UpdateRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IRelationFieldMappingBusinessRules businessRules)
    : IRequestHandler<UpdateRelationFieldMappingCommand, BaseResponseDto<RelationFieldMappingResponseDto>>
{
    public async Task<BaseResponseDto<RelationFieldMappingResponseDto>> Handle(
        UpdateRelationFieldMappingCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var entity = await repository.GetByIdAsync(dto.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<RelationFieldMappingResponseDto>.Fail("RelationFieldMapping not found");

        // ====== Business Rules ======
        await businessRules.EnsureRelationExistsAsync(dto.RelationId, cancellationToken);
        await businessRules.EnsureFieldsExistAsync(dto.FromFieldId, dto.ToFieldId, cancellationToken);
        await businessRules.EnsureFieldsBelongToCorrectEntitiesAsync(dto.RelationId, dto.FromFieldId, dto.ToFieldId, cancellationToken);
        await businessRules.EnsureFieldTypesCompatibleAsync(dto.FromFieldId, dto.ToFieldId, cancellationToken);

        if (entity.RelationId != dto.RelationId ||
            entity.FromFieldId != dto.FromFieldId ||
            entity.ToFieldId != dto.ToFieldId)
        {
            await businessRules.EnsureNoDuplicateMappingAsync(dto.RelationId, dto.FromFieldId, dto.ToFieldId, cancellationToken);
        }

        mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<RelationFieldMappingResponseDto>(entity);
        return BaseResponseDto<RelationFieldMappingResponseDto>.Ok(response, "RelationFieldMapping updated successfully");
    }
}
