using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class FieldValidator : BaseEntity
    {
        public Guid FeildId { get; set; }
        public Field Field { get; set; } = default!;
        public string ValidatorType { get; set; } = default!;
        public string? Config {  get; set; }
    }
}
