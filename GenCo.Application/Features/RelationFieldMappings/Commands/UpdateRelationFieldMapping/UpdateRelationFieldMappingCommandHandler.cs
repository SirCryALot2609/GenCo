using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.UpdateRelationFieldMapping;

public class UpdateRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateRelationFieldMappingCommand, BaseResponseDto<RelationFieldMappingResponseDto>>
{
    public async Task<BaseResponseDto<RelationFieldMappingResponseDto>> Handle(
        UpdateRelationFieldMappingCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<RelationFieldMappingResponseDto>.Fail("RelationFieldMapping not found");

        mapper.Map(request.Request, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationFieldMappingResponseDto>(entity);
        return BaseResponseDto<RelationFieldMappingResponseDto>.Ok(dto, "RelationFieldMapping updated successfully");
    }
}