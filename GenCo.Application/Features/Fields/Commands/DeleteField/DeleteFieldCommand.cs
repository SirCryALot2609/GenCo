using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.DeleteField;
public record DeleteFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;