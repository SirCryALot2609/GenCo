using FluentValidation;
using GenCo.Application.Features.EntityConstraintFields.Commands.RestoreEntityConstraintField;

namespace GenCo.Application.Validators.EntityConstraintField;

public class RestoreEntityConstraintFieldCommandValidator 
    : AbstractValidator<RestoreEntityConstraintFieldCommand>
{
    public RestoreEntityConstraintFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ConstraintField Id is required.");
    }
}
