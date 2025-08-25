using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace GenCo.Domain.Entities
{
    public class FieldValidator : BaseEntity
    {
        public Guid FieldId { get; set; }
        public virtual Field Field { get; set; } = default!;

        public ValidatorType Type { get; set; }

        // JSON string lưu trong DB
        public string? ConfigJson { get; set; }

        [NotMapped]
        public FieldValidatorConfig? ConfigObject
        {
            get => string.IsNullOrWhiteSpace(ConfigJson) ? null : JsonSerializer.Deserialize<FieldValidatorConfig>(ConfigJson);
            set => ConfigJson = value is null ? null : JsonSerializer.Serialize(value);
        }
    }
}
