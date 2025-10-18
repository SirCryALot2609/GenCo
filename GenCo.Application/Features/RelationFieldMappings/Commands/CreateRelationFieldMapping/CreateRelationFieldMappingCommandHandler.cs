using AutoMapper;
using GenCo.Application.BusinessRules.RelationFieldMappings;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.CreateRelationFieldMapping;

public class CreateRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IRelationFieldMappingBusinessRules businessRules)
    : IRequestHandler<CreateRelationFieldMappingCommand, BaseResponseDto<RelationFieldMappingResponseDto>>
{
    public async Task<BaseResponseDto<RelationFieldMappingResponseDto>> Handle(
        CreateRelationFieldMappingCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        // ====== Business Rules ======
        await businessRules.EnsureRelationExistsAsync(dto.RelationId, cancellationToken);
        await businessRules.EnsureFieldsExistAsync(dto.FromFieldId, dto.ToFieldId, cancellationToken);
        await businessRules.EnsureFieldsBelongToCorrectEntitiesAsync(dto.RelationId, dto.FromFieldId, dto.ToFieldId, cancellationToken);
        await businessRules.EnsureNoDuplicateMappingAsync(dto.RelationId, dto.FromFieldId, dto.ToFieldId, cancellationToken);
        await businessRules.EnsureFieldTypesCompatibleAsync(dto.FromFieldId, dto.ToFieldId, cancellationToken);

        // ====== Create Entity ======
        var entity = mapper.Map<RelationFieldMapping>(dto);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<RelationFieldMappingResponseDto>(entity);
        return BaseResponseDto<RelationFieldMappingResponseDto>.Ok(response, "RelationFieldMapping created successfully");
    }
}
