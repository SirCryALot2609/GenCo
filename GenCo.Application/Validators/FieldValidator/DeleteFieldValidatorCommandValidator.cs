using FluentValidation;
using GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator;

namespace GenCo.Application.Validators.FieldValidator;

public class DeleteFieldValidatorCommandValidator 
    : AbstractValidator<DeleteFieldValidatorCommand>
{
    public DeleteFieldValidatorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("FieldValidator Id is required.");
    }
}