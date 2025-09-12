using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.SoftDeleteRelationJoinTable;

public class SoftDeleteRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteRelationJoinTableCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationJoinTable not found");

        await repository.SoftDeleteAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationJoinTable soft deleted successfully");
    }
}