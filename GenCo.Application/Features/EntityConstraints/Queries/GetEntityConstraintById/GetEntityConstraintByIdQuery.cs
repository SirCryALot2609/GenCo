using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Response;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Queries.GetEntityConstraintById;

public record GetEntityConstraintByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<EntityConstraintDetailDto>>;