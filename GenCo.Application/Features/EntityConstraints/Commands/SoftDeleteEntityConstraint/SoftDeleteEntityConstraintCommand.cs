using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.SoftDeleteEntityConstraint;

public record SoftDeleteEntityConstraintCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;