using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.Relation;

namespace GenCo.Application.DTOs.Project.Responses;

public class ProjectDetailDto : ProjectBaseDto
{
    public ICollection<EntityBaseDto> Entities { get; set; } = [];
    public ICollection<RelationBaseDto> Relations { get; set; } = [];
    // public ICollection<WorkflowBaseDto> Workflows { get; set; } = [];
    // public ICollection<UIConfigBaseDto> UiConfigs { get; set; } = [];
    // public ICollection<ServiceConfigBaseDto> ServiceConfigs { get; set; } = [];
    // public ICollection<ConnectionBaseDto> Connections { get; set; } = [];
}