namespace GenCo.GenCo.Application.DTOs.RelationFieldMapping.Responses;

public class RelationFieldMappingDetailDto : RelationFieldMappingBaseDto
{
    public RelationBaseDto Relation { get; set; } = default!;
    public FieldBaseDto FromField { get; set; } = default!;
    public FieldBaseDto ToField { get; set; } = default!;
}
