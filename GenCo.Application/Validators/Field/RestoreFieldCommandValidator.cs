using FluentValidation;
using GenCo.Application.Features.Fields.Commands.RestoreField;

namespace GenCo.Application.Validators.Field;

public class RestoreFieldCommandValidator : AbstractValidator<RestoreFieldCommand>
{
    public RestoreFieldCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Field Id is required.");
    }
}