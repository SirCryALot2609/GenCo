using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Requests
{
    public class SearchEntitiesRequestDto : BaseRequestDto
    {
        public Guid ProjectId { get; set; }
        public string Keyword { get; set; } = string.Empty;
    }
}
