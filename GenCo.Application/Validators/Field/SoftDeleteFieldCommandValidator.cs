using FluentValidation;
using GenCo.Application.Features.Fields.Commands.SoftDeleteField;

namespace GenCo.Application.Validators.Field;

public class SoftDeleteFieldCommandValidator : AbstractValidator<SoftDeleteFieldCommand>
{
    public SoftDeleteFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Field Id is required.");
    }
}