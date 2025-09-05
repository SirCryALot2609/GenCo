using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class RelationJoinTable : BaseEntity
{
    public Guid RelationId { get; set; }
    public virtual Relation Relation { get; set; } = null!;

    public required string JoinTableName { get; set; }
    public required string LeftKey { get; set; }
    public required string RightKey { get; set; }
}