using FluentValidation;
using GenCo.Application.Features.EntityConstraintFields.Commands.SoftDeleteEntityConstraintField;

namespace GenCo.Application.Validators.EntityConstraintField;

public class SoftDeleteEntityConstraintFieldCommandValidator 
    : AbstractValidator<SoftDeleteEntityConstraintFieldCommand>
{
    public SoftDeleteEntityConstraintFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ConstraintField Id is required.");
    }
}