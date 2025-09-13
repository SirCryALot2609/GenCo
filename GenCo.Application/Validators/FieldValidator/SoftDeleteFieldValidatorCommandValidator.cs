using FluentValidation;
using GenCo.Application.Features.FieldValidators.Commands.SoftDeleteFieldValidator;

namespace GenCo.Application.Validators.FieldValidator;

public class SoftDeleteFieldValidatorCommandValidator 
    : AbstractValidator<SoftDeleteFieldValidatorCommand>
{
    public SoftDeleteFieldValidatorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("FieldValidator Id is required.");
    }
}