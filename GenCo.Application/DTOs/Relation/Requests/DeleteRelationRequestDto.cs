using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation.Requests
{
    public class DeleteRelationRequestDto : BaseRequestDto
    {
        public Guid Id { get; set; }
    }
}
