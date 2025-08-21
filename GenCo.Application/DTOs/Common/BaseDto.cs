using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Common
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
    }

    public abstract class BaseRequestDto
    {
       
    }

    public abstract class BaseResponseDto : BaseDto
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public abstract class BaseActionResponseDto : BaseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class BaseCreateResponseDto : BaseActionResponseDto
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class BaseUpdateResponseDto : BaseActionResponseDto
    {
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class BaseDeleteResponseDto : BaseActionResponseDto
    {
        public DateTime? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }
    }
}
