using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class Field : BaseEntity
    {
        public Guid EntityId { get; set; }
        public Entity Entity { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string? DefaultValue { get; set; }
        public ICollection<FieldValidator> Validators { get; set; } = [];
    }
}
