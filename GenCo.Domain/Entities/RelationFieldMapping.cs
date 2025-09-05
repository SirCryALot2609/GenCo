using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class RelationFieldMapping : BaseEntity
{
    public Guid RelationId { get; set; }
    public virtual Relation Relation { get; set; } = null!;

    public Guid FromFieldId { get; set; }
    public virtual Field FromField { get; set; } = null!;

    public Guid ToFieldId { get; set; }
    public virtual Field ToField { get; set; } = null!;
}