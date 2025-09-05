using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
namespace GenCo.Application.Features.Entities.Commands.UpdateEntity;
public record UpdateEntityCommand(UpdateEntityRequestDto Request)
    : IRequest<BaseResponseDto<EntityResponseDto>>;