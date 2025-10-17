using AutoMapper;
using GenCo.Application.BusinessRules.Relations;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using GenCo.Application.DTOs.Relation.Responses;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation;
public class CreateRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IRelationBusinessRules businessRules)
    : IRequestHandler<CreateRelationCommand, BaseResponseDto<RelationResponseDto>>
{
    public async Task<BaseResponseDto<RelationResponseDto>> Handle(
        CreateRelationCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        await businessRules.EnsureRelationTypeValidAsync(dto.RelationType);
        await businessRules.EnsureDeleteBehaviorValidAsync(dto.OnDelete);
        await businessRules.EnsureEntitiesExistAsync(dto.FromEntityId, dto.ToEntityId, cancellationToken);
        await businessRules.EnsureNoCircularRelationAsync(dto.FromEntityId, dto.ToEntityId);
        await businessRules.EnsureRelationUniqueOnCreateAsync(
            dto.ProjectId, dto.FromEntityId, dto.ToEntityId, dto.RelationType, cancellationToken);
        await businessRules.EnsureRelationNameValidAsync(dto.RelationName);

        var relation = mapper.Map<Relation>(dto);
        relation.Id = Guid.NewGuid();
        relation.CreatedAt = DateTime.UtcNow;

        await businessRules.EnsureFieldMappingConsistencyAsync(relation);
        await businessRules.EnsureJoinTableConsistencyAsync(relation);
        await businessRules.EnsureDeleteBehaviorCompatibilityAsync(relation);

        await repository.AddAsync(relation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<RelationResponseDto>(relation);
        return BaseResponseDto<RelationResponseDto>.Ok(response, "Relation created successfully");
    }
}
