using FluentValidation;
using GenCo.Application.Features.Entities.Commands.UpdateEntity;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;

namespace GenCo.Application.Validators.Entity;

public class UpdateEntityCommandValidator : AbstractValidator<UpdateEntityCommand>
{
    private readonly IGenericRepository<Domain.Entities.Entity> _repository;
    
    public UpdateEntityCommandValidator(IGenericRepository<Domain.Entities.Entity> repository)
    {
        _repository = repository;
        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Entity Id is required.");

        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Entity name is required.")
            .MaximumLength(100).WithMessage("Entity name cannot exceed 100 characters.")
            .MustAsync(BeUniqueName).WithMessage("Entity name already exists in this project.");

    }
    
    private async Task<bool> BeUniqueName(UpdateEntityCommand command, string name, CancellationToken cancellationToken)
    {
        var spec = new EntityByNameSpec(name, command.Request.ProjectId);
        var entities = await _repository.FindAsync(spec, cancellationToken: cancellationToken);
        return entities.All(e => e.Id == command.Request.Id);
    }
}
