using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.RelationJoinTable;

public class RelationJoinTableBaseDto : AuditableDto
{
    public string JoinTableName { get; set; } = null!;
    public string LeftKey { get; set; } = null!;
    public string RightKey { get; set; } = null!;
}