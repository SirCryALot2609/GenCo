using GenCo.Application.BusinessRules.RelationJoinTables;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.DeleteRelationJoinTable;

public class DeleteRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IRelationJoinTableBusinessRules businessRules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRelationJoinTableCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(
        DeleteRelationJoinTableCommand request,
        CancellationToken cancellationToken)
    {
        // 🧩 1. Đảm bảo JoinTable có thể bị xóa
        await businessRules.EnsureCanDeleteAsync(request.Id, cancellationToken);

        // 🧩 2. Xóa entity
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationJoinTable not found");

        await repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationJoinTable deleted successfully");
    }
}
