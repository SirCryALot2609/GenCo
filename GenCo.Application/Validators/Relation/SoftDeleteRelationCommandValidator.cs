using FluentValidation;
using GenCo.Application.Features.Relations.Commands.SoftDeleteRelation;

namespace GenCo.Application.Validators.Relation;

public class SoftDeleteRelationCommandValidator : AbstractValidator<SoftDeleteRelationCommand>
{
    public SoftDeleteRelationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Relation Id is required.");
    }
}