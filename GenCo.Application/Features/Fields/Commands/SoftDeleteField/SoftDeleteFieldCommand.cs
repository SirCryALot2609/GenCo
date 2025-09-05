using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.SoftDeleteField;

public record SoftDeleteFieldCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;