using FluentValidation;
using GenCo.Application.Features.EntityConstraintFields.Commands.UpdateEntityConstraintField;

namespace GenCo.Application.Validators.EntityConstraintField;

public class UpdateEntityConstraintFieldCommandValidator 
    : AbstractValidator<UpdateEntityConstraintFieldCommand>
{
    public UpdateEntityConstraintFieldCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("ConstraintField Id is required.");

        RuleFor(x => x.Request.ConstraintId)
            .NotEmpty().WithMessage("ConstraintId is required.");

        RuleFor(x => x.Request.FieldId)
            .NotEmpty().WithMessage("FieldId is required.");
    }
}