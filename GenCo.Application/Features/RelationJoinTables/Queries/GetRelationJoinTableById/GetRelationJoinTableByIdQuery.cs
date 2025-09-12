using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Queries.GetRelationJoinTableById;

public record GetRelationJoinTableByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<RelationJoinTableDetailDto>>;