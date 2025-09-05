using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class Field : BaseEntity
{
    public Guid EntityId { get; set; }
    public virtual Entity Entity { get; set; } = null!;

    public required string ColumnName { get; set; }  // Column name
    public required string Type { get; set; }        // Data type (string, int, datetime…)
    public int? Length { get; set; }
    public int? Scale { get; set; }

    public bool IsRequired { get; set; }
    public bool IsAutoIncrement { get; set; }        // Identity column
    public string? DefaultValue { get; set; }

    public string? Comment { get; set; }             // For documentation
    public int ColumnOrder { get; set; }             // Column order

    public virtual ICollection<FieldValidator> Validators { get; set; } = [];
}