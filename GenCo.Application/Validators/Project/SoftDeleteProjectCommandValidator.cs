using FluentValidation;
using GenCo.Application.Features.Projects.Commands.SoftDeleteProject;

namespace GenCo.Application.Validators.Project;

public class SoftDeleteProjectCommandValidator : AbstractValidator<SoftDeleteProjectCommand>
{
    public SoftDeleteProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Project Id is required.");
    }
}