using FluentValidation;
using GenCo.Application.Features.FieldValidators.Commands.RestoreFieldValidator;

namespace GenCo.Application.Validators.FieldValidator;


public class RestoreFieldValidatorCommandValidator 
    : AbstractValidator<RestoreFieldValidatorCommand>
{
    public RestoreFieldValidatorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("FieldValidator Id is required.");
    }
}