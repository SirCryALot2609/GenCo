using GenCo.Application.DTOs.Common;
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
    public class ProjectResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<EntitySummaryResponseDto> Entities { get; set; } = new List<EntitySummaryResponseDto>();
        public ICollection<RelationSummaryResponseDto> Relations { get; set; } = new List<RelationSummaryResponseDto>();
        public ICollection<WorkflowSummaryResponseDto> Workflows { get; set; } = new List<WorkflowSummaryResponseDto>();
        public ICollection<UIConfigSummaryResponseDto> UIConfigs { get; set; } = new List<UIConfigSummaryResponseDto>();
        public ICollection<ServiceConfigSummaryResponseDto> ServiceConfigs { get; set; } = new List<ServiceConfigSummaryResponseDto>();
    }
}
