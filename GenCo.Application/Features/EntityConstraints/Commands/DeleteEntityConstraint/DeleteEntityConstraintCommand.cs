using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.DeleteEntityConstraint;

public record DeleteEntityConstraintCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;