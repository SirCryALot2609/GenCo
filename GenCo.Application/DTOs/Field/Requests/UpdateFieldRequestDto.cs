using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Field.Requests;

public class UpdateFieldRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }

    public string ColumnName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int? Length { get; set; }
    public int? Scale { get; set; }

    public bool IsRequired { get; set; }
    public bool IsAutoIncrement { get; set; }
    public string? DefaultValue { get; set; }

    public string? Comment { get; set; }
    public int ColumnOrder { get; set; }
}