using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.DTOs.ServiceConfig.Responses;
using GenCo.Application.DTOs.UIConfig.Responses;
using GenCo.Application.DTOs.Workflow.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project.Responses
{
    public class ProjectDetailDto : ProjectResponseDto
    {
        public ICollection<EntityDetailsResponseDto>? Entities { get; set; }
        public ICollection<RelationResponseDto>? Relations { get; set; }
        public ICollection<WorkflowResponseDto>? Workflows { get; set; }
        public ICollection<UIConfigResponseDto>? UIConfigs { get; set; }
        public ICollection<ServiceConfigResponseDto>? ServiceConfigs { get; set; }
    }
}
