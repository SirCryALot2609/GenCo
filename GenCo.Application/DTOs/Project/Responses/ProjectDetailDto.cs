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
    public class ProjectDetailDto : ProjectBaseDto
    {
      public ICollection<EntityDetailDto> Entities { get; set; } = [];
      public ICollection<RelationDetailDto> Relations { get; set; } = [];
      public ICollection<WorkflowDetailDto> Workflows { get; set; } = [];
      public ICollection<UIConfigDetailDto> UIConfigs { get; set; } = [];
      public ICollection<ServiceConfigDetailDto> ServiceConfigs { get; set; } = [];
    }
}
