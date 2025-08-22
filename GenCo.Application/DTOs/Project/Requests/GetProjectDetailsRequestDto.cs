using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project.Requests
{
    public class GetProjectDetailsRequestDto : BaseRequestDto
    {
        public Guid ProjectId { get; set; }
        public bool IncludeMetadata { get; set; } = false;
        public bool IncludeEntities { get; set; } = false;
        public bool IncludeRelations { get; set; } = false;
        public bool IncludeConnections { get; set; } = false;
        public bool IncludeWorkflows { get; set; } = false;
    }
}
