using AutoMapper;
using GenCo.Application.BusinessRules.Relations;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Relations.Commands.UpdateRelation;
public class UpdateRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IRelationBusinessRules businessRules)
    : IRequestHandler<UpdateRelationCommand, BaseResponseDto<RelationResponseDto>>
{
    public async Task<BaseResponseDto<RelationResponseDto>> Handle(
        UpdateRelationCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var relation = await repository.GetByIdAsync(dto.Id, cancellationToken: cancellationToken);
        if (relation == null)
            return BaseResponseDto<RelationResponseDto>.Fail("Relation not found");

        await businessRules.EnsureRelationTypeValidAsync(dto.RelationType);
        await businessRules.EnsureDeleteBehaviorValidAsync(dto.OnDelete);
        await businessRules.EnsureEntitiesExistAsync(dto.FromEntityId, dto.ToEntityId, cancellationToken);
        await businessRules.EnsureNoCircularRelationAsync(dto.FromEntityId, dto.ToEntityId);
        await businessRules.EnsureRelationUniqueOnUpdateAsync(
            dto.ProjectId, dto.FromEntityId, dto.ToEntityId, dto.Id, dto.RelationType, cancellationToken);
        await businessRules.EnsureRelationNameValidAsync(dto.RelationName);

        mapper.Map(dto, relation);
        relation.UpdatedAt = DateTime.UtcNow;

        await businessRules.EnsureFieldMappingConsistencyAsync(relation);
        await businessRules.EnsureJoinTableConsistencyAsync(relation);
        await businessRules.EnsureDeleteBehaviorCompatibilityAsync(relation);

        await repository.UpdateAsync(relation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<RelationResponseDto>(relation);
        return BaseResponseDto<RelationResponseDto>.Ok(response, "Relation updated successfully");
    }
}
