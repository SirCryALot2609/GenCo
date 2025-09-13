using FluentValidation;
using GenCo.Application.Features.Relations.Commands.UpdateRelation;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Relations;

namespace GenCo.Application.Validators.Relation;

public class UpdateRelationCommandValidator : AbstractValidator<UpdateRelationCommand>
{
    private readonly IGenericRepository<Domain.Entities.Relation> _repository;

    public UpdateRelationCommandValidator(IGenericRepository<Domain.Entities.Relation> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Relation Id is required.");

        RuleFor(x => x.Request.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");

        RuleFor(x => x.Request.FromEntityId)
            .NotEmpty().WithMessage("FromEntityId is required.");

        RuleFor(x => x.Request.ToEntityId)
            .NotEmpty().WithMessage("ToEntityId is required.")
            .NotEqual(x => x.Request.FromEntityId).WithMessage("FromEntityId and ToEntityId cannot be the same.");

        RuleFor(x => x.Request.RelationType)
            .IsInEnum().WithMessage("Invalid RelationType.");

        RuleFor(x => x.Request.OnDelete)
            .IsInEnum().WithMessage("Invalid DeleteBehavior.");

        RuleFor(x => x.Request.RelationName)
            .MaximumLength(100).WithMessage("RelationName cannot exceed 100 characters.")
            .MustAsync(BeUniqueName).WithMessage("Relation name already exists in this project.");
    }

    private async Task<bool> BeUniqueName(UpdateRelationCommand command, string? name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;

        var spec = new RelationByNameSpec(name, command.Request.ProjectId);
        var relations = await _repository.FindAsync(spec, cancellationToken: cancellationToken);
        return relations.All(r => r.Id == command.Request.Id);
    }
}
