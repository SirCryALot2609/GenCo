namespace GenCo.Application.DTOs.Common;

public abstract class BaseDto
{
    public Guid Id { get; set; }
}

public abstract class AuditableDto : BaseDto
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

public abstract class BaseRequestDto
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public DateTimeOffset RequestedAt { get; set; } = DateTimeOffset.UtcNow;

    public string? TraceId { get; set; }
    public Guid? UserId { get; set; }
}

public class BaseResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "Success";
    public string? ErrorCode { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }   // ✅ thêm

    public static BaseResponseDto<T> Ok(T data, string? message = null)
        => new()
        {
            Success = true,
            Message = message ?? "Success",
            Data = data
        };

    public static BaseResponseDto<T> Fail(string message, string? errorCode = null)
        => new()
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Data = default
        };

    public static BaseResponseDto<T> Fail(IEnumerable<string> errors, string? errorCode = null)
        => new()
        {
            Success = false,
            Message = "Validation errors occurred",
            ErrorCode = errorCode ?? "VALIDATION_ERROR",
            Errors = errors.ToList()
        };
}

public class PagedResponseDto<T> : BaseResponseDto<IReadOnlyCollection<T>>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

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

    public new static PagedResponseDto<T> Fail(string message, string? errorCode = null)
        => new()
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Data = Array.Empty<T>(),
            TotalCount = 0,
            PageNumber = 0,
            PageSize = 0
        };
}

