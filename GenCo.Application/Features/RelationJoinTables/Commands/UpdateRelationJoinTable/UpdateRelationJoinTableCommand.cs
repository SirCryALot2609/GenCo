using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Requests;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.UpdateRelationJoinTable;

public record UpdateRelationJoinTableCommand(UpdateRelationJoinTableRequestDto Request)
    : IRequest<BaseResponseDto<RelationJoinTableResponseDto>>;