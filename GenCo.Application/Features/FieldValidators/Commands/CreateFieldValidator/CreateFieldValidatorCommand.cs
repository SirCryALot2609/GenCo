using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Requests;
using GenCo.Application.DTOs.FieldValidator.Responses;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.CreateFieldValidator;
public record CreateFieldValidatorCommand(CreateFieldValidatorRequestDto Request)
    : IRequest<BaseResponseDto<FieldValidatorResponseDto>>;