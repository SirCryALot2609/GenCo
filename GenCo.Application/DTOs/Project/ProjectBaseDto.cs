using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project
{
    public abstract class ProjectBaseDto : AuditableDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
