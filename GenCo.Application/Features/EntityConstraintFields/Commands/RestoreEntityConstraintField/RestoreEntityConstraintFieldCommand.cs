using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.RestoreEntityConstraintField;

public record RestoreEntityConstraintFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;