using FluentValidation;
using GenCo.Application.Features.Projects.Commands.UpdateProject;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;

namespace GenCo.Application.Validators.Project;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    private readonly IGenericRepository<Domain.Entities.Project> _repository;

    public UpdateProjectCommandValidator(IGenericRepository<Domain.Entities.Project> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Project Id is required.");

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(200).WithMessage("Project name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("Project name already exists.");
    }

    private async Task<bool> BeUniqueName(
        UpdateProjectCommand command,
        string name,
        CancellationToken cancellationToken)
    {
        var spec = new ProjectByNameSpec(name);
        var projects = await _repository.FindAsync(spec, cancellationToken: cancellationToken);
        return projects.All(p => p.Id == command.Request.Id);
    }
}