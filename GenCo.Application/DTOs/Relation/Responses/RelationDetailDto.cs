using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation.Responses
{
    public class RelationDetailDto : RelationBaseDto
    {
        public ProjectBaseDto Project { get; set; } = default!;
        public EntityBaseDto FromEntity { get; set; } = default!;
        public EntityBaseDto ToEntity { get; set; } = default!;
    }
}
