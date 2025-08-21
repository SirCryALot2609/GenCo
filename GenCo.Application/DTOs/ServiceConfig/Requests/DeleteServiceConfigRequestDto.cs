using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.ServiceConfig.Requests
{
    public class DeleteServiceConfigRequestDto : BaseRequestDto
    {
        public Guid Id { get; set; }
    }
}
