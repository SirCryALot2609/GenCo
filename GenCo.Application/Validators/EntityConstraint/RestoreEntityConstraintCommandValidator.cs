using FluentValidation;
using GenCo.Application.Features.EntityConstraints.Commands.RestoreEntityConstraint;

namespace GenCo.Application.Validators.EntityConstraint;

public class RestoreEntityConstraintCommandValidator 
    : AbstractValidator<RestoreEntityConstraintCommand>
{
    public RestoreEntityConstraintCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Constraint Id is required.");
    }
}