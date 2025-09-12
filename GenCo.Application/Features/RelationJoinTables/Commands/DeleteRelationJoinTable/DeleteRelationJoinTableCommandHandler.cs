using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.DeleteRelationJoinTable;

public class DeleteRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRelationJoinTableCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationJoinTable not found");

        await repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationJoinTable deleted successfully");
    }
}