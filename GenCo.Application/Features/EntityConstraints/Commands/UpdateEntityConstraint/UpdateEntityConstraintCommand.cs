using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Requests;
using GenCo.Application.DTOs.EntityConstraint.Response;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.UpdateEntityConstraint;

public record UpdateEntityConstraintCommand(UpdateEntityConstraintRequestDto Request)
    : IRequest<BaseResponseDto<EntityConstraintResponseDto>>;