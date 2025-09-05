using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator;
public record DeleteFieldValidatorCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
