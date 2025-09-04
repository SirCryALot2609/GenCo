using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.RelationFieldMapping;

public class RelationFieldMappingBaseDto : AuditableDto
{
    public Guid RelationId { get; set; }
    public Guid FromFieldId { get; set; }
    public Guid ToFieldId { get; set; }
}
