using GenCo.Application.DTOs.Field;
using GenCo.Application.DTOs.Relation;

namespace GenCo.Application.DTOs.RelationFieldMapping.Responses;

public class RelationFieldMappingDetailDto : RelationFieldMappingBaseDto
{
    public FieldBaseDto FromField { get; set; } = null!;
    public FieldBaseDto ToField { get; set; } = null!;
    public RelationBaseDto Relation { get; set; } = null!;
}
