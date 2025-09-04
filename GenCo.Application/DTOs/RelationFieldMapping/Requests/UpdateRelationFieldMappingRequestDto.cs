using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.RelationFieldMapping.Requests;

public class UpdateRelationFieldMappingRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid RelationId { get; set; }
    public Guid FromFieldId { get; set; }
    public Guid ToFieldId { get; set; }
}
