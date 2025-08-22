using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.DTOs.Workflow.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project.Responses
{
    public class ProjectDetailsResponseDto : ProjectResponseDto
    {
        // metadata
        public IDictionary<string, string>? Metadata { get; set; }

        // navigation objects
        public ICollection<EntityDetailsResponseDto>? Entities { get; set; }
        public ICollection<RelationResponseDto>? Relations { get; set; }
        public ICollection<WorkflowResponseDto>? Workflows { get; set; }
    }
}
