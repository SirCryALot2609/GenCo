using FluentValidation;
using GenCo.Application.Features.Entities.Commands.CreateEntity;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;

namespace GenCo.Application.Validators.Entity;

public class CreateEntityCommandValidator : AbstractValidator<CreateEntityCommand>
{
    private readonly IGenericRepository<Domain.Entities.Entity> _repository;

    public CreateEntityCommandValidator(IGenericRepository<Domain.Entities.Entity> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Entity name is required.")
            .MaximumLength(100).WithMessage("Entity name must not exceed 100 characters.")
            .MustAsync(BeUniqueName).WithMessage("Entity name already exists in this project.");
    }

    private async Task<bool> BeUniqueName(
        CreateEntityCommand command,
        string name,
        CancellationToken cancellationToken)
    {
        var spec = new EntityByNameSpec(name, command.Request.ProjectId);
        return !await _repository.ExistsAsync(spec, cancellationToken);
    }
}
