using FluentValidation;
using GenCo.Application.Features.Fields.Commands.CreateField;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Fields;

namespace GenCo.Application.Validators.Field;

public class CreateFieldCommandValidator : AbstractValidator<CreateFieldCommand>
{
    private readonly IGenericRepository<Domain.Entities.Field> _repository;

    public CreateFieldCommandValidator(IGenericRepository<Domain.Entities.Field> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.EntityId)
            .NotEmpty().WithMessage("EntityId is required.");

        RuleFor(x => x.Request.ColumnName)
            .NotEmpty().WithMessage("Column name is required.")
            .MaximumLength(100).WithMessage("Column name cannot exceed 100 characters.");

        RuleFor(x => x.Request.Type)
            .NotEmpty().WithMessage("Field type is required.");
    }
}

