using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.RestoreRelationJoinTable;

public record RestoreRelationJoinTableCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;