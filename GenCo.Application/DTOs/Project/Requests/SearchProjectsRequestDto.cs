using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project.Requests
{
    public class SearchProjectsRequestDto : BaseRequestDto
    {
        public string Keyword { get; set; } = string.Empty;
    }
}
