using FluentValidation;
using GenCo.Application.Features.Relations.Commands.DeleteRelation;

namespace GenCo.Application.Validators.Relation;

public class DeleteRelationCommandValidator : AbstractValidator<DeleteRelationCommand>
{
    public DeleteRelationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Relation Id is required.");
    }
}
