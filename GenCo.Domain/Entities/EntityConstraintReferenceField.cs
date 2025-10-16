using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class EntityConstraintReferenceField : BaseEntity
{
    public Guid ConstraintId { get; set; }
    public virtual EntityConstraint Constraint { get; set; } = null!;

    public Guid ReferencedFieldId { get; set; }
    public virtual Field ReferencedField { get; set; } = null!;
}
