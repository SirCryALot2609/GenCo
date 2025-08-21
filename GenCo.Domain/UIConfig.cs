using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class UIConfig : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
        public string PageType { get; set; } = default!;
        public string? Config { get; set; }
    }
}
