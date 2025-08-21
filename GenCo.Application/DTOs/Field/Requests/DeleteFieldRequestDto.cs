using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Requests
{
    public class DeleteFieldRequestDto : BaseRequestDto
    {
        public Guid Id { get; set; }
    }
}
