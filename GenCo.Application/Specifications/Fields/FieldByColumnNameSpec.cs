using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Fields;

public class FieldByColumnNameSpec : BaseSpecification<Field>
{
    public FieldByColumnNameSpec(string columnName, Guid entityId)
        : base(f => f.ColumnName == columnName && f.EntityId == entityId)
    {
        AddInclude(f => f.Entity);
    }
}
