using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation.Responses;
using MediatR;

namespace GenCo.Application.Features.Relations.Queries.GetRelationById;
public record GetRelationByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<RelationDetailDto>>;
