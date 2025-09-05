using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Requests;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.UpdateEntityConstraintField;

public record UpdateEntityConstraintFieldCommand(UpdateEntityConstraintFieldRequestDto Request)
    : IRequest<BaseResponseDto<EntityConstraintFieldResponseDto>>;