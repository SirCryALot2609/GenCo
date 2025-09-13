using FluentValidation;
using GenCo.Application.DTOs.RelationFieldMapping.Requests;
using GenCo.Application.Features.RelationFieldMappings.Commands.CreateRelationFieldMapping;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.RelationFieldMappings;

namespace GenCo.Application.Validators.RelationFieldMapping;

public class CreateRelationFieldMappingCommandValidator 
    : AbstractValidator<CreateRelationFieldMappingCommand>
{
    private readonly IGenericRepository<Domain.Entities.RelationFieldMapping> _repository;

    public CreateRelationFieldMappingCommandValidator(
        IGenericRepository<Domain.Entities.RelationFieldMapping> repository)
    {
        _repository = repository;

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
        CreateRelationFieldMappingRequestDto request,
        CancellationToken cancellationToken)
    {
        var spec = new RelationFieldMappingByKeysSpec(request.RelationId, request.FromFieldId, request.ToFieldId);
        return !await _repository.ExistsAsync(spec, cancellationToken);
    }
}
