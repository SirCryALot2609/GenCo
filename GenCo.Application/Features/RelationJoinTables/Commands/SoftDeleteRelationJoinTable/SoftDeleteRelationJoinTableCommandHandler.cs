using GenCo.Application.BusinessRules.RelationJoinTables;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.SoftDeleteRelationJoinTable;

public class SoftDeleteRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IRelationJoinTableBusinessRules businessRules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteRelationJoinTableCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        // 🧩 1. Đảm bảo có thể xóa
        await businessRules.EnsureCanDeleteAsync(request.Id, cancellationToken);

        // 🧩 2. Tìm entity
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationJoinTable not found");

        // 🧩 3. Đánh dấu xóa mềm
        await repository.SoftDeleteAsync(entity, cancellationToken);
        entity.UpdatedAt = DateTime.UtcNow;

        // 🧩 4. Lưu thay đổi
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationJoinTable soft deleted successfully");
    }
}
