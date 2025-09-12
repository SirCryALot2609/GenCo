using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.SoftDeleteRelationJoinTable;

public record SoftDeleteRelationJoinTableCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
    