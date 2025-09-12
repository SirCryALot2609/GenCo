using FluentValidation;
using GenCo.Application.Features.EntityConstraints.Commands.CreateEntityConstraint;
using GenCo.Domain.Enum;

namespace GenCo.Application.Validators.EntityConstraint;

public class CreateEntityConstraintCommandValidator 
    : AbstractValidator<CreateEntityConstraintCommand>
{
    public CreateEntityConstraintCommandValidator()
    {
        RuleFor(x => x.Request.EntityId)
            .NotEmpty().WithMessage("EntityId is required.");

        RuleFor(x => x.Request.Type)
            .NotEmpty().WithMessage("Constraint type is required.")
            .Must(BeValidConstraintType)
            .WithMessage("Invalid constraint type.");

        RuleFor(x => x.Request.ConstraintName)
            .NotEmpty().WithMessage("Constraint name is required.")
            .MaximumLength(100).WithMessage("Constraint name cannot exceed 100 characters.");
    }

    private bool BeValidConstraintType(string type)
    {
        return Enum.TryParse<ConstraintType>(type, ignoreCase: true, out _);
    }
}
