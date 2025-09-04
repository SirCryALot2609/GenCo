using GenCo.Application.DTOs.Field;
using GenCo.Application.DTOs.Relation;

namespace GenCo.Application.DTOs.RelationFieldMapping.Responses;

public class RelationFieldMappingDetailDto : RelationFieldMappingBaseDto
{
    public RelationBaseDto Relation { get; set; } = default!;
    public FieldBaseDto FromField { get; set; } = default!;
    public FieldBaseDto ToField { get; set; } = default!;
}
