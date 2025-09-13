using FluentValidation;
using GenCo.Application.DTOs.RelationFieldMapping.Requests;
using GenCo.Application.Features.RelationFieldMappings.Commands.UpdateRelationFieldMapping;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.RelationFieldMappings;

namespace GenCo.Application.Validators.RelationFieldMapping;

public class UpdateRelationFieldMappingCommandValidator 
    : AbstractValidator<UpdateRelationFieldMappingCommand>
{
    private readonly IGenericRepository<Domain.Entities.RelationFieldMapping> _repository;

    public UpdateRelationFieldMappingCommandValidator(
        IGenericRepository<Domain.Entities.RelationFieldMapping> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Mapping Id is required.");

        RuleFor(x => x.Request.RelationId)
            .NotEmpty().WithMessage("RelationId is required.");

        RuleFor(x => x.Request.FromFieldId)
            .NotEmpty().WithMessage("FromFieldId is required.");

        RuleFor(x => x.Request.ToFieldId)
            .NotEmpty().WithMessage("ToFieldId is required.")
            .NotEqual(x => x.Request.FromFieldId)
            .WithMessage("ToFieldId cannot be the same as FromFieldId.");

        RuleFor(x => x.Request)
            .MustAsync(BeUniqueMapping).WithMessage("This field mapping already exists.");
    }

    private async Task<bool> BeUniqueMapping(
        UpdateRelationFieldMappingRequestDto request,
        CancellationToken cancellationToken)
    {
        var spec = new RelationFieldMappingByKeysSpec(request.RelationId, request.FromFieldId, request.ToFieldId);
        var mappings = await _repository.FindAsync(spec, cancellationToken: cancellationToken);
        return mappings.All(m => m.Id == request.Id);
    }
}
