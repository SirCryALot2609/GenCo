using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.RelationJoinTable.Requests;

public class CreateRelationJoinTableRequestDto : BaseRequestDto
{
    public Guid RelationId { get; set; }
    public string JoinTableName { get; set; } = null!;
    public string LeftKey { get; set; } = null!;
    public string RightKey { get; set; } = null!;
}