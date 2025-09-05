using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Commands.UpdateEntityConstraintField;

public class UpdateEntityConstraintFieldCommandHandler(
    IGenericRepository<EntityConstraintField> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateEntityConstraintFieldCommand, BaseResponseDto<EntityConstraintFieldResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintFieldResponseDto>> Handle(UpdateEntityConstraintFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<EntityConstraintFieldResponseDto>.Fail("EntityConstraintField not found");

        mapper.Map(request.Request, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityConstraintFieldResponseDto>(entity);
        return BaseResponseDto<EntityConstraintFieldResponseDto>.Ok(dto, "EntityConstraintField updated successfully");
    }
}