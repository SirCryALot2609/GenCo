using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Project.Requests;

public class UpdateProjectRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}