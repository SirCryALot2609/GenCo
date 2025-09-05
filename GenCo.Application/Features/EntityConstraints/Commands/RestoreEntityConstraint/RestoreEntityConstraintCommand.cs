using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.RestoreEntityConstraint;

public record RestoreEntityConstraintCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;