using AutoMapper;
using GenCo.Application.BusinessRules.Fields;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.CreateField;

public class CreateFieldCommandHandler(
    IGenericRepository<Field> repository,
    IFieldBusinessRules rules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateFieldCommand, BaseResponseDto<FieldResponseDto>>
{
    public async Task<BaseResponseDto<FieldResponseDto>> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
    {
        await rules.EnsureEntityExistsAsync(request.Request.EntityId, cancellationToken);
        await rules.EnsureFieldNameUniqueOnCreateAsync(request.Request.EntityId, request.Request.ColumnName, cancellationToken);
        await rules.EnsureFieldTypeValidAsync(request.Request.Type);

        var field = mapper.Map<Field>(request.Request);
        field.Id = Guid.NewGuid();
        field.CreatedAt = DateTime.UtcNow;
        field.UpdatedAt = field.CreatedAt;

        await rules.EnsureFieldConfigurationValidAsync(field);
        await rules.EnsureValidatorsValidAsync(field);

        await repository.AddAsync(field, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            throw new BusinessRuleValidationException("Failed to save field", "SAVE_FAILED");

        var dto = mapper.Map<FieldResponseDto>(field);
        return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field created successfully");
    }
}
