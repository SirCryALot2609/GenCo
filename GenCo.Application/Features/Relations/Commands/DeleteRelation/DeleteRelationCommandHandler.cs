using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation;
public class DeleteRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRelationCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
    {
        var relation = await repository.GetByIdAsync(request.Id, cancellationToken : cancellationToken);
        if (relation == null)
            return BaseResponseDto<bool>.Fail("Relation not found");

        await repository.DeleteAsync(relation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Relation deleted successfully");
    }
}
