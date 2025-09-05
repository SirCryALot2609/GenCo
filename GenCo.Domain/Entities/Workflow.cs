using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class Workflow : BaseEntity
{
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;
    public required string Name { get; set; }
    public virtual ICollection<WorkflowStep> Steps { get; set; } = [];
}