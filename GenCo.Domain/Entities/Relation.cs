using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;

namespace GenCo.Domain.Entities;

public class Relation : BaseEntity
{
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public Guid FromEntityId { get; set; }
    public virtual Entity FromEntity { get; set; } = null!;

    public Guid ToEntityId { get; set; }
    public virtual Entity ToEntity { get; set; } = null!;

    public RelationType RelationType { get; set; }
    public DeleteBehavior OnDelete { get; set; }

    public string? RelationName { get; set; }

    public virtual ICollection<RelationFieldMapping> FieldMappings { get; set; } = [];

    // Many-to-Many support (JoinTable)
    public virtual ICollection<RelationJoinTable> JoinTables { get; set; } = [];
}