using FluentValidation;
using GenCo.Application.Features.RelationJoinTables.Commands.SoftDeleteRelationJoinTable;

namespace GenCo.Application.Validators.RelationJoinTable;

public class SoftDeleteRelationJoinTableCommandValidator : AbstractValidator<SoftDeleteRelationJoinTableCommand>
{
    public SoftDeleteRelationJoinTableCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}