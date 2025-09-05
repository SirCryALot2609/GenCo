using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
namespace GenCo.Application.Features.Entities.Commands.RestoreEntity;
public record RestoreEntityCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;