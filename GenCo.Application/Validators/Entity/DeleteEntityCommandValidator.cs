using FluentValidation;
using GenCo.Application.Features.Entities.Commands.DeleteEntity;

namespace GenCo.Application.Validators.Entity;

public class DeleteEntityCommandValidator : AbstractValidator<DeleteEntityCommand>
{
    public DeleteEntityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Entity Id is required.");
    }
}