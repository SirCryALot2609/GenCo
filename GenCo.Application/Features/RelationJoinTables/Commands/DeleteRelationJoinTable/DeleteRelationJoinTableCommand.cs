using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.DeleteRelationJoinTable;

public record DeleteRelationJoinTableCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
