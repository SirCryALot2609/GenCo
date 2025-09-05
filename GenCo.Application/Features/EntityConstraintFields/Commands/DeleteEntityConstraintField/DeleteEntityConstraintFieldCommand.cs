using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.DeleteEntityConstraintField;

public record DeleteEntityConstraintFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;