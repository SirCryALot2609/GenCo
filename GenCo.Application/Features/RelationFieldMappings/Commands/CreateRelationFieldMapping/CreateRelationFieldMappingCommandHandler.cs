using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.CreateRelationFieldMapping;

public class CreateRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateRelationFieldMappingCommand, BaseResponseDto<RelationFieldMappingResponseDto>>
{
    public async Task<BaseResponseDto<RelationFieldMappingResponseDto>> Handle(
        CreateRelationFieldMappingCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<RelationFieldMapping>(request.Request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationFieldMappingResponseDto>(entity);
        return BaseResponseDto<RelationFieldMappingResponseDto>.Ok(dto, "RelationFieldMapping created successfully");
    }
}