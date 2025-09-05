using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Fields.Commands.UpdateField;

public class UpdateFieldCommandHandler(
    IGenericRepository<Field> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateFieldCommand, BaseResponseDto<FieldResponseDto>>
{
    public async Task<BaseResponseDto<FieldResponseDto>> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (field == null)
            return BaseResponseDto<FieldResponseDto>.Fail("Field not found");

        mapper.Map(request.Request, field);
        field.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(field, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldResponseDto>(field);
        return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field updated successfully");
    }
}