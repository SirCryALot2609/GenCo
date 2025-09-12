using GenCo.Application.DTOs.Relation;

namespace GenCo.Application.DTOs.RelationJoinTable.Responses;

public class RelationJoinTableDetailDto : RelationJoinTableBaseDto
{
    public RelationBaseDto Relation { get; set; } = null!;
}