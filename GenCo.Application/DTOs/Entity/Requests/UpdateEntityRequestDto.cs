using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Entity.Requests;

public class UpdateEntityRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = default!;
    public string? Label { get; set; }
}