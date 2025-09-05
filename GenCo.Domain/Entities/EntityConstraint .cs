using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;

namespace GenCo.Domain.Entities;

public class EntityConstraint : BaseEntity
{
    public Guid EntityId { get; set; }
    public virtual Entity Entity { get; set; } = null!;
    public ConstraintType Type { get; set; }
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; } // Check constraint expression
    public virtual ICollection<EntityConstraintField> Fields { get; set; } = [];
}