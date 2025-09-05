using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.EntityConstraintFields;

public class EntityConstraintFieldByIdSpec : BaseSpecification<EntityConstraintField>
{
    public EntityConstraintFieldByIdSpec(Guid id, bool includeDetails = false)
        : base(ecf => ecf.Id == id)
    {
        if (!includeDetails) return;
        AddInclude(ecf => ecf.Constraint); // Load EntityConstraint
        AddInclude(ecf => ecf.Field);      // Load Field
    }
}