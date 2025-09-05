using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using MediatR;

namespace GenCo.Application.Features.Entities.Commands.DeleteEntity;

public record DeleteEntityCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;