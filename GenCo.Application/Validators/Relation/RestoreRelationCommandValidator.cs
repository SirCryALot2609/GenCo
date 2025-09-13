using FluentValidation;
using GenCo.Application.Features.Relations.Commands.ResoreRelation;

namespace GenCo.Application.Validators.Relation;

public class RestoreRelationCommandValidator : AbstractValidator<RestoreRelationCommand>
{
    public RestoreRelationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Relation Id is required.");
    }
}
