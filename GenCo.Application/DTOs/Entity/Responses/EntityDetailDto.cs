using GenCo.Application.DTOs.Field;
using GenCo.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Responses
{
    public class EntityDetailDto : EntityBaseDto
    {
        public ProjectBaseDto Project { get; set; } = default!;

        public ICollection<FieldBaseDto> Fields { get; set; } = [];
        public ICollection<RelationDto> FromRelations { get; set; } = [];
        public ICollection<RelationDto> ToRelations { get; set; } = [];
    }
}
