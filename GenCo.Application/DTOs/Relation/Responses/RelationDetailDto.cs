using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.Project;
using GenCo.Application.DTOs.RelationFieldMapping;

namespace GenCo.Application.DTOs.Relation.Responses;

public class RelationDetailDto : RelationBaseDto
{
    public ProjectBaseDto Project { get; set; } = default!;
    public EntityBaseDto FromEntity { get; set; } = default!;
    public EntityBaseDto ToEntity { get; set; } = default!;
    public ICollection<RelationFieldMappingBaseDto> FieldMappings { get; set; } = [];
}