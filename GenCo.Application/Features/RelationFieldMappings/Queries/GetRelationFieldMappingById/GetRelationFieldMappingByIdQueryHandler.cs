using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.RelationFieldMappings;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Queries.GetRelationFieldMappingById;

public class GetRelationFieldMappingByIdQueryHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IMapper mapper)
    : IRequestHandler<GetRelationFieldMappingByIdQuery, BaseResponseDto<RelationFieldMappingDetailDto>>
{
    public async Task<BaseResponseDto<RelationFieldMappingDetailDto>> Handle(
        GetRelationFieldMappingByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Sử dụng specification để include các relation/field nếu cần
        var spec = new RelationFieldMappingByIdSpec(request.Id, request.IncludeDetails);
        var mapping = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (mapping == null)
            return BaseResponseDto<RelationFieldMappingDetailDto>.Fail("RelationFieldMapping not found");

        var dto = mapper.Map<RelationFieldMappingDetailDto>(mapping);
        return BaseResponseDto<RelationFieldMappingDetailDto>.Ok(dto, "RelationFieldMapping retrieved successfully");
    }
}