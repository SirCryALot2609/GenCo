using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class WorkflowStep : BaseEntity
{
    public Guid WorkflowId { get; set; }
    public virtual Workflow Workflow { get; set; } = null!;

    public required string StepType { get; set; }
    public string? Config { get; set; }

    public int StepOrder { get; set; }   // NEW: để chạy workflow theo thứ tự
}