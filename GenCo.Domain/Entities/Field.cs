using GenCo.Domain.Entities.Common;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class Field : BaseEntity
    {
        public Guid EntityId { get; set; }
        public virtual Entity Entity { get; set; } = default!;

        public string ColumnName { get; set; } = default!; // Column name
        public string Type { get; set; } = default!;       // Data type (string, int, datetime…)
        public int? Length { get; set; }
        public int? Scale { get; set; }

        public bool IsRequired { get; set; }
        public bool IsAutoIncrement { get; set; }          // Identity column
        public string? DefaultValue { get; set; }

        public string? Comment { get; set; }               // For documentation
        public int ColumnOrder { get; set; }               // Column order

        public virtual ICollection<FieldValidator> Validators { get; set; } = [];
    }
}
