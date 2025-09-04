namespace GenCo.GenCo.Application.DTOs.EntityContraint.Response;

public class EntityConstraintBaseDto : AuditableDto
{
    public Guid EntityId { get; set; }
    public ConstraintType Type { get; set; }
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; }
}
