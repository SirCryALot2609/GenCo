using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Workflow.Requests
{
    public class DeleteWorkflowRequestDto : BaseRequestDto
    {
        public int Id { get; set; }
    }
}
