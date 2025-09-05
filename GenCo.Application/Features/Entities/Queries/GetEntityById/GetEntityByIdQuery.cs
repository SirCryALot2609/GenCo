using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
namespace GenCo.Application.Features.Entities.Queries.GetEntityById;
public record GetEntityByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<EntityDetailDto>>;