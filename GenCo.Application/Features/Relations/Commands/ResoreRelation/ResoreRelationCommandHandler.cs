using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Relations.Commands.ResoreRelation;
public class RestoreRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreRelationCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreRelationCommand request, CancellationToken cancellationToken)
    {
        var relation = await repository.GetByIdAsync(request.Id, cancellationToken : cancellationToken);
        if (relation == null)
            return BaseResponseDto<bool>.Fail("Relation not found");

        await repository.RestoreAsync(relation, cancellationToken);
        relation.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return BaseResponseDto<bool>.Ok(true, "Relation restored successfully");
    }
}
