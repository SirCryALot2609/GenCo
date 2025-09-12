using FluentValidation;
using GenCo.Application.Features.Fields.Commands.DeleteField;

namespace GenCo.Application.Validators.Field;

public class DeleteFieldCommandValidator : AbstractValidator<DeleteFieldCommand>
{
    public DeleteFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Field Id is required.");
    }
}