using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;

namespace GenCo.Application.DTOs.Relation.Requests;

public class CreateRelationRequestDto : BaseRequestDto
{
    public Guid ProjectId { get; set; }
    public Guid FromEntityId { get; set; }
    public Guid ToEntityId { get; set; }
    public string RelationType { get; set; } = string.Empty;
    public string OnDelete { get; set; } = string.Empty;
    public string? RelationName { get; set; }
}