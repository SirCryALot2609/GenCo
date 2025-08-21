using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation.Requests
{
    public class CreateRelationRequestDto : BaseRequestDto
    {
        public int ProjectId { get; set; }
        public int FromEntityId { get; set; }
        public int ToEntityId { get; set; }
        public string RelationType { get; set; } = default!; // OneToMany, ManyToMany...
    }
}
