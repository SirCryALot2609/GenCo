using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.Specifications.EntityConstraints;

public class EntityConstraintsByTypeSpec(Guid entityId, ConstraintType type, Guid? excludeConstraintId = null)
    : BaseSpecification<EntityConstraint>(c => c.EntityId == entityId
                                               && c.Type == type
                                               && (excludeConstraintId == null || c.Id != excludeConstraintId));