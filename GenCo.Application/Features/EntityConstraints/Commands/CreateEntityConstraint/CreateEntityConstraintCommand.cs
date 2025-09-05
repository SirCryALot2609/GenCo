using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Requests;
using GenCo.Application.DTOs.EntityConstraint.Response;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.CreateEntityConstraint;

public record CreateEntityConstraintCommand(CreateEntityConstraintRequestDto Request)
    : IRequest<BaseResponseDto<EntityConstraintResponseDto>>;