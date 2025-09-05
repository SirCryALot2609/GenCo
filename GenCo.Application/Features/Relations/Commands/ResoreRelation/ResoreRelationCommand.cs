using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Relations.Commands.ResoreRelation;
public record RestoreRelationCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
