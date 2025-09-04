using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation
{
    public class RelationBaseDto : AuditableDto
    {
      public Guid ProjectId { get; set; }
      public Guid FromEntityId { get; set; }
      public Guid ToEntityId { get; set; }

      public RelationType RelationType { get; set; }
      public DeleteBehavior OnDelete { get; set; }
      public string? RelationName { get; set; }
    }
}
