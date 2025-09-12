using FluentValidation;
using GenCo.Application.Features.EntityConstraintFields.Commands.CreateEntityConstraintField;

namespace GenCo.Application.Validators.EntityConstraintField;

public class CreateEntityConstraintFieldCommandValidator 
    : AbstractValidator<CreateEntityConstraintFieldCommand>
{
    public CreateEntityConstraintFieldCommandValidator()
    {
        RuleFor(x => x.Request.ConstraintId)
            .NotEmpty().WithMessage("ConstraintId is required.");

        RuleFor(x => x.Request.FieldId)
            .NotEmpty().WithMessage("FieldId is required.");
    }
}