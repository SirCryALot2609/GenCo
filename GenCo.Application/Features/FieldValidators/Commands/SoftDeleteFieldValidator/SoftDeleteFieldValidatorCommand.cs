using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.SoftDeleteFieldValidator;
public record SoftDeleteFieldValidatorCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
