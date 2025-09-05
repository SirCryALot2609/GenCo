using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.DeleteRelationFieldMapping;

public class DeleteRelationFieldMappingCommandHandler(
    IGenericRepository<RelationFieldMapping> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRelationFieldMappingCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteRelationFieldMappingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<bool>.Fail("RelationFieldMapping not found");

        await repository.DeleteAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "RelationFieldMapping deleted successfully");
    }
}
