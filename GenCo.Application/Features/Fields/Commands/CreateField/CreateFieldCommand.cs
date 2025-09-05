using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Requests;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.CreateField;

public record CreateFieldCommand(CreateFieldRequestDto Request)
    : IRequest<BaseResponseDto<FieldResponseDto>>;