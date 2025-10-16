using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.EntityConstraintFields;

public class EntityConstraintFieldByConstraintAndFieldSpec : BaseSpecification<EntityConstraintField>
{
    public EntityConstraintFieldByConstraintAndFieldSpec(Guid constraintId, Guid fieldId)
        : base(ecf => ecf.ConstraintId == constraintId && ecf.FieldId == fieldId)
    {
        AddInclude(ecf => ecf.Constraint);
        AddInclude(ecf => ecf.Field);
    }
}