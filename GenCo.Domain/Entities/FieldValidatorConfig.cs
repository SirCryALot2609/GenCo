namespace GenCo.Domain.Entities;

public class FieldValidatorConfig
{
    public string? RegexPattern { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string? CustomMessage { get; set; }
}