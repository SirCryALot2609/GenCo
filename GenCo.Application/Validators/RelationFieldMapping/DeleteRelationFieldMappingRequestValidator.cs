using FluentValidation;
using GenCo.Application.Features.RelationFieldMappings.Commands.DeleteRelationFieldMapping;

namespace GenCo.Application.Validators.RelationFieldMapping;

public class DeleteRelationFieldMappingRequestValidator : AbstractValidator<DeleteRelationFieldMappingCommand>
{
    public DeleteRelationFieldMappingRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}