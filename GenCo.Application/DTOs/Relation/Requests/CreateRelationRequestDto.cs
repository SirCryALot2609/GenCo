using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;

namespace GenCo.Application.DTOs.Relation.Requests;

public class CreateRelationRequestDto : BaseRequestDto
{
    public Guid ProjectId { get; set; }
    public Guid FromEntityId { get; set; }
    public Guid ToEntityId { get; set; }
    public RelationType RelationType { get; set; }
    public DeleteBehavior OnDelete { get; set; }
    public string? RelationName { get; set; }
}