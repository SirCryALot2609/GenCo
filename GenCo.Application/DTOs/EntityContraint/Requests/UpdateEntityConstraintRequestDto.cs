namespace GenCo.GenCo.Application.DTOs.EntityContraint.Requests;

public class UpdateEntityConstraintRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public ConstraintType Type { get; set; }
    public string ConstraintName { get; set; } = default!;
    public string? Expression { get; set; }
}
