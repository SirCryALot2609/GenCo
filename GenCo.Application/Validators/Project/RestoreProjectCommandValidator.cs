using FluentValidation;
using GenCo.Application.Features.Projects.Commands.RestoreProject;

namespace GenCo.Application.Validators.Project;

public class RestoreProjectCommandValidator : AbstractValidator<RestoreProjectCommand>
{
    public RestoreProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Project Id is required.");
    }
}