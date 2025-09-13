using FluentValidation;
using GenCo.Application.Features.RelationFieldMappings.Commands.SoftDeleteRelationFieldMapping;

namespace GenCo.Application.Validators.RelationFieldMapping;

public class SoftDeleteRelationFieldMappingRequestValidator : AbstractValidator<SoftDeleteRelationFieldMappingCommand>
{
    public SoftDeleteRelationFieldMappingRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}