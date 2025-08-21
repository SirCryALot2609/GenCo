using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation.Responses
{
    public class RelationResponseDto : BaseResponseDto
    {
        public int ProjectId { get; set; }
        public int FromEntityId { get; set; }
        public int ToEntityId { get; set; }
        public string RelationType { get; set; } = default!;
    }
}
