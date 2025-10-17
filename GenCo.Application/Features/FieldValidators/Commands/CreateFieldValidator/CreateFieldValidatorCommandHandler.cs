using AutoMapper;
using GenCo.Application.BusinessRules.FieldValidators;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.CreateFieldValidator;
public sealed class CreateFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> validatorRepository,
    IFieldValidatorBusinessRules businessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
{
    public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(
        CreateFieldValidatorCommand request,
        CancellationToken cancellationToken)
    {
        var fieldId = request.Request.FieldId;
        var type = request.Request.Type;

        // ✅ Validate domain rules
        await businessRules.EnsureFieldExistsAsync(fieldId, cancellationToken);
        await businessRules.EnsureValidatorTypeValidAsync(type);
        await businessRules.EnsureValidatorUniqueOnCreateAsync(fieldId, type, cancellationToken);

        var validator = mapper.Map<FieldValidator>(request.Request);
        validator.Id = Guid.NewGuid();
        validator.CreatedAt = DateTime.UtcNow;

        await businessRules.EnsureValidatorConfigValidAsync(validator);

        await validatorRepository.AddAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldValidatorResponseDto>(validator);
        return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "FieldValidator created successfully");
    }
}
