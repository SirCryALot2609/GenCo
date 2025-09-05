namespace GenCo.Domain.Enum;

public enum ValidatorType
{
    Unknown,    
    Required,    // Không cho null/empty
    Regex,       // Regex pattern
    Range,       // Min/Max (numeric)
    Email,       // Email format
    MinLength,   // Min length string
    MaxLength,   // Max length string
    Custom       // Custom validation function
}