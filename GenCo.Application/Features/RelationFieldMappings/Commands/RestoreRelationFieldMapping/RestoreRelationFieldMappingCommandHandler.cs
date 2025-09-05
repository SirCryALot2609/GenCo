using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.RestoreRelationFieldMapping;

public class RestoreRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreRelationFieldMappingCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreRelationFieldMappingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationFieldMapping not found");

        await repository.RestoreAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationFieldMapping restored successfully");
    }
}