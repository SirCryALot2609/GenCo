using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Enum
{
    public enum ValidatorType
    {
        Required,    // Không cho null/empty
        Regex,       // Regex pattern
        Range,       // Min/Max (numeric)
        Email,       // Email format
        MinLength,   // Min length string
        MaxLength,   // Max length string
        Custom       // Custom validation function
    }
}
