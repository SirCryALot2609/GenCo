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
        public Guid ProjectId { get; set; }
        public Guid FromEntityId { get; set; }
        public Guid ToEntityId { get; set; }
        public string RelationType { get; set; } = default!; // OneToMany, ManyToMany...
    }
}
