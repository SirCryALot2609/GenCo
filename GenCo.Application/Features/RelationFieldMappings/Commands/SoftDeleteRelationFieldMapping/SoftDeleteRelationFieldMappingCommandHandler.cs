using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.SoftDeleteRelationFieldMapping;

public class SoftDeleteRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteRelationFieldMappingCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteRelationFieldMappingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationFieldMapping not found");

        await repository.SoftDeleteAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationFieldMapping soft deleted successfully");
    }
}