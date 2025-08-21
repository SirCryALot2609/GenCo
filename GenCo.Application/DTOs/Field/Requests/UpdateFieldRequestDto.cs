using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Requests
{
    public class UpdateFieldRequestDto : BaseRequestDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public bool? IsNullable { get; set; }
        public bool? IsPrimaryKey { get; set; }
    }
}
