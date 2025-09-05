using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class EntityConstraintField : BaseEntity
{
    public Guid ConstraintId { get; set; }
    public virtual EntityConstraint Constraint { get; set; } = null!;

    public Guid FieldId { get; set; }
    public virtual Field Field { get; set; } = null!;
}