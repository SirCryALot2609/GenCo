using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace GenCo.Domain.Entities;

public class FieldValidator : BaseEntity
{
    public Guid FieldId { get; set; }
    public virtual Field Field { get; set; } = null!;

    public ValidatorType Type { get; set; }

    public string? ConfigJson { get; set; }

    [NotMapped]
    public FieldValidatorConfig? ConfigObject
    {
        get => string.IsNullOrWhiteSpace(ConfigJson) ? null : JsonSerializer.Deserialize<FieldValidatorConfig>(ConfigJson);
        set => ConfigJson = value is null ? null : JsonSerializer.Serialize(value);
    }
}