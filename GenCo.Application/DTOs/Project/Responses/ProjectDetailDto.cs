using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Relation.Responses;


namespace GenCo.Application.DTOs.Project.Responses;

public class ProjectDetailDto : ProjectBaseDto
{
    public ICollection<EntityDetailDto> Entities { get; set; } = [];
    public ICollection<RelationDetailDto> Relations { get; set; } = [];
}