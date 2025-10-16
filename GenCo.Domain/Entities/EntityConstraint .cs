using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;

namespace GenCo.Domain.Entities;

public class EntityConstraint : BaseEntity
{
    public Guid EntityId { get; set; }
    public virtual Entity Entity { get; set; } = null!;

    public ConstraintType Type { get; set; }
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; } // For CHECK constraints

    // ===== Foreign Key Reference =====
    public Guid? ReferencedEntityId { get; set; }  // Entity được tham chiếu (nếu là FK)
    public virtual Entity? ReferencedEntity { get; set; }

    public virtual ICollection<EntityConstraintField> Fields { get; set; } = [];

    // (Tuỳ chọn) Nếu muốn thể hiện cả các field được tham chiếu bên bảng kia:
    public virtual ICollection<EntityConstraintReferenceField> ReferencedFields { get; set; } = [];
}
