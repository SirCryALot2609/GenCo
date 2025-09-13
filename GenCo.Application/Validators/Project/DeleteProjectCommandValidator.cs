using FluentValidation;
using GenCo.Application.Features.Projects.Commands.DeleteProject;

namespace GenCo.Application.Validators.Project;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Project Id is required.");
    }
}