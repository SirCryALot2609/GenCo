using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById;

public record GetFieldByIdQuery(Guid Id, bool IncludeValidators = false)
    : IRequest<BaseResponseDto<FieldDetailDto>>;