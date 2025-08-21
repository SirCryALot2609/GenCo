using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class Entity : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Label { get; set; }
        public ICollection<Field> Fields { get; set; } = [];
    }
}
