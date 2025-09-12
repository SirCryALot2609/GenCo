using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Requests;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.CreateRelationJoinTable;

public record CreateRelationJoinTableCommand(CreateRelationJoinTableRequestDto Request)
    : IRequest<BaseResponseDto<RelationJoinTableResponseDto>>;