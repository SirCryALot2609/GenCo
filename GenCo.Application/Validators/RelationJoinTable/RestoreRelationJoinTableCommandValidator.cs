using FluentValidation;
using GenCo.Application.Features.RelationJoinTables.Commands.RestoreRelationJoinTable;

namespace GenCo.Application.Validators.RelationJoinTable;

public class RestoreRelationJoinTableCommandValidator : AbstractValidator<RestoreRelationJoinTableCommand>
{
    public RestoreRelationJoinTableCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}