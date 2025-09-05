using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.SoftDeleteEntityConstraintField;

public record SoftDeleteEntityConstraintFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;