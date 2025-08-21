using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class WorkflowStep : BaseEntity
    {
        public Guid WorkflowId { get; set; }
        public Workflow Workflow { get; set; } = default!;
        public string StepType { get; set; } = default!;
        public string? Config { get; set; }
    }
}
