using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.RestoreField;

public record RestoreFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;