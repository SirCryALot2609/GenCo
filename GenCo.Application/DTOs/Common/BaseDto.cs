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

    public class BoolResultDto
    {
        public bool Value { get; set; }
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

    public abstract class BaseRequestDto
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    public class BaseResponseDto<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static BaseResponseDto<T> Ok(T data, string? message = null)
            => new() { Success = true, Message = message ?? "Success", Data = data };

        public static BaseResponseDto<T> Fail(string message)
            => new() { Success = false, Message = message };
    }

    public class PagedResponseDto<T> : BaseResponseDto<IReadOnlyCollection<T>>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // Factory helpers
        public static PagedResponseDto<T> Ok(
            IReadOnlyCollection<T> items,
            int totalCount,
            int pageNumber,
            int pageSize,
            string? message = null)
            => new()
            {
                Success = true,
                Message = message ?? "Success",
                Data = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        public static PagedResponseDto<T> Fail(string message)
            => new()
            {
                Success = false,
                Message = message,
                Data = [],
                TotalCount = 0,
                PageNumber = 0,
                PageSize = 0
            };
    }
}
