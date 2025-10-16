using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Fields;

public class FieldByEntityAndNameSpec : BaseSpecification<Field>
{
    public FieldByEntityAndNameSpec(
        Guid entityId,
        string columnName,
        Guid? excludeFieldId = null,
        bool includeValidators = false)
        : base(f =>
            f.EntityId == entityId &&
            f.ColumnName == columnName &&
            (excludeFieldId == null || f.Id != excludeFieldId))
    {
        if (includeValidators)
        {
            AddInclude(f => f.Validators);
        }

        AddInclude(f => f.Entity);
    }
}
