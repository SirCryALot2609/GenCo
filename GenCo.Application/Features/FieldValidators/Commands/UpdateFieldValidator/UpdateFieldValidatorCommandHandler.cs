using AutoMapper;
using GenCo.Application.BusinessRules.FieldValidators;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.UpdateFieldValidator;
public sealed class UpdateFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> validatorRepository,
    IFieldValidatorBusinessRules businessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
{
    public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(
        UpdateFieldValidatorCommand request,
        CancellationToken cancellationToken)
    {
        var validatorId = request.Request.Id;
        var fieldId = request.Request.FieldId;
        var type = request.Request.Type;

        await businessRules.EnsureValidatorExistsAsync(validatorId, cancellationToken);
        await businessRules.EnsureFieldExistsAsync(fieldId, cancellationToken);
        await businessRules.EnsureValidatorBelongsToFieldAsync(validatorId, fieldId, cancellationToken);
        await businessRules.EnsureValidatorTypeValidAsync(type);
        await businessRules.EnsureValidatorUniqueOnUpdateAsync(fieldId, validatorId, type, cancellationToken);

        var validator = await validatorRepository.GetByIdAsync(validatorId, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<FieldValidatorResponseDto>.Fail("FieldValidator not found");

        mapper.Map(request.Request, validator);
        validator.UpdatedAt = DateTime.UtcNow;

        await businessRules.EnsureValidatorConfigValidAsync(validator);

        await validatorRepository.UpdateAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldValidatorResponseDto>(validator);
        return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "FieldValidator updated successfully");
    }
}
