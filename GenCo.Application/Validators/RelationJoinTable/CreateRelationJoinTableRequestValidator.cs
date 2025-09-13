using FluentValidation;
using GenCo.Application.DTOs.RelationJoinTable.Requests;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.RelationJoinTables;

namespace GenCo.Application.Validators.RelationJoinTable;

public class CreateRelationJoinTableRequestValidator : AbstractValidator<CreateRelationJoinTableRequestDto>
{
    private readonly IGenericRepository<Domain.Entities.RelationJoinTable> _repository;

    public CreateRelationJoinTableRequestValidator(IGenericRepository<Domain.Entities.RelationJoinTable> repository)
    {
        _repository = repository;

        RuleFor(x => x.RelationId)
            .NotEmpty().WithMessage("RelationId is required.");

        RuleFor(x => x.JoinTableName)
            .NotEmpty().WithMessage("JoinTableName is required.")
            .MaximumLength(200).WithMessage("JoinTableName cannot exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("JoinTableName already exists in this relation.");

        RuleFor(x => x.LeftKey)
            .NotEmpty().WithMessage("LeftKey is required.")
            .MaximumLength(100).WithMessage("LeftKey cannot exceed 100 characters.");

        RuleFor(x => x.RightKey)
            .NotEmpty().WithMessage("RightKey is required.")
            .MaximumLength(100).WithMessage("RightKey cannot exceed 100 characters.")
            .NotEqual(x => x.LeftKey).WithMessage("LeftKey and RightKey cannot be the same.");
    }

    private async Task<bool> BeUniqueName(CreateRelationJoinTableRequestDto dto, string joinTableName, CancellationToken ct)
    {
        var spec = new RelationJoinTableByNameSpec(dto.RelationId, joinTableName);
        return !await _repository.ExistsAsync(spec, ct);
    }
}