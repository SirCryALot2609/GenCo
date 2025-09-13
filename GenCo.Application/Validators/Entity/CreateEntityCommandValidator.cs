using FluentValidation;
using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.Features.Entities.Commands.CreateEntity;

namespace GenCo.Application.Validators.Entity;

public class CreateEntityCommandValidator : AbstractValidator<CreateEntityCommand>
{
    public CreateEntityCommandValidator()
    {
        RuleFor(x => x.Request.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Entity name is required.")
            .MaximumLength(100).WithMessage("Entity name must not exceed 100 characters.");
    }
}
