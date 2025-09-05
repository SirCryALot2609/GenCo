using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Queries.GetRelationFieldMappingById;

public record GetRelationFieldMappingByIdQuery(Guid Id, bool IncludeDetails = false) 
    : IRequest<BaseResponseDto<RelationFieldMappingDetailDto>>;