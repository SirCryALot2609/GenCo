using FluentValidation;
using GenCo.Application.Features.EntityConstraints.Commands.SoftDeleteEntityConstraint;

namespace GenCo.Application.Validators.EntityConstraint;

public class SoftDeleteEntityConstraintCommandValidator 
    : AbstractValidator<SoftDeleteEntityConstraintCommand>
{
    public SoftDeleteEntityConstraintCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Constraint Id is required.");
    }
}