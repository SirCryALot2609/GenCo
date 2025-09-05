using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
namespace GenCo.Application.Features.Entities.Commands.CreateEntity;
public record CreateEntityCommand(CreateEntityRequestDto Request)
    : IRequest<BaseResponseDto<EntityResponseDto>>;