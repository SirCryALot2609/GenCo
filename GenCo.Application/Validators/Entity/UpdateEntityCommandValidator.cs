using FluentValidation;
using GenCo.Application.BusinessRules.Entities;
using GenCo.Application.Features.Entities.Commands.UpdateEntity;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;

namespace GenCo.Application.Validators.Entity;

public class UpdateEntityCommandValidator : AbstractValidator<UpdateEntityCommand>
{
    public UpdateEntityCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Entity Id is required.");

        RuleFor(x => x.Request.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Entity name is required.")
            .MaximumLength(100).WithMessage("Entity name cannot exceed 100 characters.");
    }
}

