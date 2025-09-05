using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Queries.GetEntityConstraintFieldById;

public record GetEntityConstraintFieldByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<EntityConstraintFieldDetailDto>>;