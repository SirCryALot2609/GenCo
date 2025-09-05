using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Relations.Commands.SoftDeleteRelation;
public record SoftDeleteRelationCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
