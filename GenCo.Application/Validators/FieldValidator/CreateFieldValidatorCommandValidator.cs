using FluentValidation;
using GenCo.Application.Features.FieldValidators.Commands.CreateFieldValidator;

namespace GenCo.Application.Validators.FieldValidator;

public class CreateFieldValidatorCommandValidator 
    : AbstractValidator<CreateFieldValidatorCommand>
{
    public CreateFieldValidatorCommandValidator()
    {
        RuleFor(x => x.Request.FieldId)
            .NotEmpty().WithMessage("FieldId is required.");

        RuleFor(x => x.Request.Type)
            .NotEmpty().WithMessage("Validator type is required.")
            .MaximumLength(100).WithMessage("Validator type must not exceed 100 characters.");

        RuleFor(x => x.Request.ConfigObject)
            .NotNull().WithMessage("Validator config is required.");
    }
}