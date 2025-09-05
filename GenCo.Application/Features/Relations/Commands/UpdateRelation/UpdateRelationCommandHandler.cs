using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Relations.Commands.UpdateRelation;
public class UpdateRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateRelationCommand, BaseResponseDto<RelationResponseDto>>
{
    public async Task<BaseResponseDto<RelationResponseDto>> Handle(
        UpdateRelationCommand request,
        CancellationToken cancellationToken)
    {
        var relation = await repository.GetByIdAsync(request.Request.Id, cancellationToken : cancellationToken);
        if (relation == null)
            return BaseResponseDto<RelationResponseDto>.Fail("Relation not found");

        mapper.Map(request.Request, relation);
        relation.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(relation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationResponseDto>(relation);
        return BaseResponseDto<RelationResponseDto>.Ok(dto, "Relation updated successfully");
    }
}
