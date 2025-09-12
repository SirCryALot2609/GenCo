using FluentValidation;
using GenCo.Application.Features.EntityConstraintFields.Commands.DeleteEntityConstraintField;

namespace GenCo.Application.Validators.EntityConstraintField;

public class DeleteEntityConstraintFieldCommandValidator 
    : AbstractValidator<DeleteEntityConstraintFieldCommand>
{
    public DeleteEntityConstraintFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ConstraintField Id is required.");
    }
}