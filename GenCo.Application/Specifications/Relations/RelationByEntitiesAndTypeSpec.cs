using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.Specifications.Relations;

public sealed class RelationByEntitiesAndTypeSpec(
    Guid projectId,
    Guid fromEntityId,
    Guid toEntityId,
    RelationType relationType,
    Guid? excludeRelationId = null)
    : BaseSpecification<Relation>(r =>
        r.ProjectId == projectId &&
        r.FromEntityId == fromEntityId &&
        r.ToEntityId == toEntityId &&
        r.RelationType == relationType &&
        (excludeRelationId == null || r.Id != excludeRelationId));