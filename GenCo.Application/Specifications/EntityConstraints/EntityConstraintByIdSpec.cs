using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.EntityConstraints;

public class EntityConstraintByIdSpec : BaseSpecification<EntityConstraint>
{
    public EntityConstraintByIdSpec(Guid constraintId, bool includeDetails = false)
        : base(c => c.Id == constraintId)
    {
        if (includeDetails)
        {
            AddInclude(c => c.Fields);                  // load Fields
            AddInclude(c => c.Fields.Select(f => f.Field)); // load Field navigation nếu có
        }

        AddInclude(c => c.Entity); // luôn include Entity
    }
}