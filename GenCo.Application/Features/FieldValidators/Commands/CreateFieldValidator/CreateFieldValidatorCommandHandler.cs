using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.FieldValidators.Commands.CreateFieldValidator;
public class CreateFieldValidatorCommandHandler(
    IGenericRepository<FieldValidator> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
{
    public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(
        CreateFieldValidatorCommand request,
        CancellationToken cancellationToken)
    {
        var validator = mapper.Map<FieldValidator>(request.Request);
        validator.Id = Guid.NewGuid();
        validator.CreatedAt = DateTime.UtcNow;
        validator.UpdatedAt = null;

        await repository.AddAsync(validator, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldValidatorResponseDto>(validator);
        return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "FieldValidator created successfully");
    }
}
