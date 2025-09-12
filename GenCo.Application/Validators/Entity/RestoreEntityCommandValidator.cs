using FluentValidation;
using GenCo.Application.Features.Entities.Commands.RestoreEntity;

namespace GenCo.Application.Validators.Entity;

public class RestoreEntityCommandValidator : AbstractValidator<RestoreEntityCommand>
{
    public RestoreEntityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Entity Id is required.");
    }
}
