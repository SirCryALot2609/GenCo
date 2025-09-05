using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Project;

public class ProjectBaseDto : AuditableDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}