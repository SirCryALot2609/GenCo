using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.EntityConstraints;

public class EntityConstraintByNameAndEntitySpec(Guid entityId, string name, Guid? excludeConstraintId = null)
    : BaseSpecification<EntityConstraint>(c => c.EntityId == entityId
                                               && c.ConstraintName == name
                                               && (excludeConstraintId == null || c.Id != excludeConstraintId));