using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Project.Requests;

public class CreateProjectRequestDto : BaseRequestDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}