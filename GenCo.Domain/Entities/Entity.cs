using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class Entity : BaseEntity
{
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public required string Name { get; set; }   // Table name
    public string? Label { get; set; }          // UI display name
    public string? Schema { get; set; }         // Optional: schema name

    public virtual ICollection<Field> Fields { get; set; } = [];
    public virtual ICollection<EntityConstraint> Constraints { get; set; } = [];
    public virtual ICollection<Relation> FromRelations { get; set; } = [];
    public virtual ICollection<Relation> ToRelations { get; set; } = [];
}