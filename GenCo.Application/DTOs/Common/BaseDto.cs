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

    public abstract class AuditableDto : BaseDto
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }

    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message ?? "Success",
                Data = data
            };
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = null
            };
        }
    }

    public abstract class BaseRequestDto
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }

    public abstract class BaseResponseDto : AuditableDto
    {
    }
}
