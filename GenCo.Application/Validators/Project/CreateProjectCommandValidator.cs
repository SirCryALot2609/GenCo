using FluentValidation;
using GenCo.Application.Features.Projects.Commands.CreateProject;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;

namespace GenCo.Application.Validators.Project;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    private readonly IGenericRepository<Domain.Entities.Project> _repository;

    public CreateProjectCommandValidator(IGenericRepository<Domain.Entities.Project> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(200).WithMessage("Project name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("Project name already exists.");
    }

    private async Task<bool> BeUniqueName(
        CreateProjectCommand command,
        string name,
        CancellationToken cancellationToken)
    {
        var spec = new ProjectByNameSpec(name);
        return !await _repository.ExistsAsync(spec, cancellationToken);
    }
}