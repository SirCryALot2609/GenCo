using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.RestoreFieldValidator;
public record RestoreFieldValidatorCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
