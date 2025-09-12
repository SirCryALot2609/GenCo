using FluentValidation;
using GenCo.Application.Features.EntityConstraints.Commands.DeleteEntityConstraint;

namespace GenCo.Application.Validators.EntityConstraint;

public class DeleteEntityConstraintCommandValidator 
    : AbstractValidator<DeleteEntityConstraintCommand>
{
    public DeleteEntityConstraintCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Constraint Id is required.");
    }
}