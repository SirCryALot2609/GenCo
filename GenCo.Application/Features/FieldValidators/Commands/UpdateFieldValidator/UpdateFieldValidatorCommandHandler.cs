using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.UpdateFieldValidator;
public class UpdateFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
{
    public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(UpdateFieldValidatorCommand request, CancellationToken cancellationToken)
    {
        var validator = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (validator == null)
            return BaseResponseDto<FieldValidatorResponseDto>.Fail("FieldValidator not found");

        mapper.Map(request.Request, validator);
        validator.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldValidatorResponseDto>(validator);
        return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "FieldValidator updated successfully");
    }
}
