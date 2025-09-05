using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Entity> Entities { get; set; } = [];
    public virtual ICollection<Relation> Relations { get; set; } = [];
    public virtual ICollection<Workflow> Workflows { get; set; } = [];
    public virtual ICollection<UIConfig> UiConfigs { get; set; } = [];
    public virtual ICollection<ServiceConfig> ServiceConfigs { get; set; } = [];
    public virtual ICollection<Connection> Connections { get; set; } = [];
}