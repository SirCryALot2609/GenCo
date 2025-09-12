using FluentValidation;
using GenCo.Application.Features.Entities.Commands.SoftDeleteEntity;

namespace GenCo.Application.Validators.Entity;

public class SoftDeleteEntityCommandValidator : AbstractValidator<SoftDeleteEntityCommand>
{
    public SoftDeleteEntityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Entity Id is required.");
    }
}