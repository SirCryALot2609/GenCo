using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;
namespace GenCo.Application.DTOs.EntityConstraint.Requests;
public class CreateEntityConstraintRequestDto : BaseRequestDto
{
    public Guid EntityId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; }
}
