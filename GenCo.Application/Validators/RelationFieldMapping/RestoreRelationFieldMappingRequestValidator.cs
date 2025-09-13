using FluentValidation;
using GenCo.Application.Features.RelationFieldMappings.Commands.RestoreRelationFieldMapping;

namespace GenCo.Application.Validators.RelationFieldMapping;

public class RestoreRelationFieldMappingRequestValidator : AbstractValidator<RestoreRelationFieldMappingCommand>
{
    public RestoreRelationFieldMappingRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}