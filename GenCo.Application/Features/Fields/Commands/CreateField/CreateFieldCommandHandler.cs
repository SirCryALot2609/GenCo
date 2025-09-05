using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Fields.Commands.CreateField;

public class CreateFieldCommandHandler(
    IGenericRepository<Field> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateFieldCommand, BaseResponseDto<FieldResponseDto>>
{
    public async Task<BaseResponseDto<FieldResponseDto>> Handle(
        CreateFieldCommand request,
        CancellationToken cancellationToken)
    {
        var field = mapper.Map<Field>(request.Request);
        field.Id = Guid.NewGuid();
        field.CreatedAt = DateTime.UtcNow;
        field.UpdatedAt = null;

        await repository.AddAsync(field, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<FieldResponseDto>(field);
        return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field created successfully");
    }
}