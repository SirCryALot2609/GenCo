using GenCo.Application.DTOs.Common;
namespace GenCo.Application.DTOs.EntityConstraint.Requests;
public class UpdateEntityConstraintRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; }
}
