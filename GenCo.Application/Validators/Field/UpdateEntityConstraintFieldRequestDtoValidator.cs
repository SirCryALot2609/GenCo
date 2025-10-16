using FluentValidation;
using GenCo.Application.DTOs.EntityConstraintField.Requests;
using GenCo.Application.Features.Fields.Commands.UpdateField;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Fields;

namespace GenCo.Application.Validators.Field;

public class UpdateFieldCommandValidator : AbstractValidator<UpdateFieldCommand>
{
    private readonly IGenericRepository<Domain.Entities.Field> _repository;

    public UpdateFieldCommandValidator(IGenericRepository<Domain.Entities.Field> repository)
    {
        _repository = repository;

        RuleFor(x => x.Request.Id)
            .NotEmpty().WithMessage("Field Id is required.");

        RuleFor(x => x.Request.ColumnName)
            .NotEmpty().WithMessage("Column name is required.")
            .MaximumLength(100).WithMessage("Column name cannot exceed 100 characters.");

        RuleFor(x => x.Request.Type)
            .NotEmpty().WithMessage("Field type is required.");
    }
}
