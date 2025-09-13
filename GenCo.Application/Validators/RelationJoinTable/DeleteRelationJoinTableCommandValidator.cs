using FluentValidation;
using GenCo.Application.Features.RelationJoinTables.Commands.DeleteRelationJoinTable;

namespace GenCo.Application.Validators.RelationJoinTable;

public class DeleteRelationJoinTableCommandValidator : AbstractValidator<DeleteRelationJoinTableCommand>
{
    public DeleteRelationJoinTableCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}