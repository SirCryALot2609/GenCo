using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.UIConfig.Responses
{
    public class UIConfigsByProjectIdResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
        public List<UIConfigResponseDto> UIConfigs { get; set; } = [];
    }
}
