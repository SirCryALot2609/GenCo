using GenCo.Application.DTOs.Common;
namespace GenCo.Application.DTOs.Entity;
public class EntityBaseDto : AuditableDto
{
    public string Name { get; set; } = default!;
    public string? Label { get; set; }
}