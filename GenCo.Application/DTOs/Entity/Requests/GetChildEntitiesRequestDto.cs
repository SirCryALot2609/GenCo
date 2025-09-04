using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.Entity.Requests;

public class GetChildEntitiesRequestDto : BaseRequestDto
{
    /// <summary>
    /// Id của Entity cha (root entity)
    /// </summary>
    public Guid ParentEntityId { get; set; }

    /// <summary>
    /// Mức độ load recursive relations (1,2,3…)
    /// </summary>
    public int PagingLevel { get; set; } = 1;

    /// <summary>
    /// Trang hiện tại
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Kích thước trang
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Có include Fields và Validators không
    /// </summary>
    public bool IncludeFieldsAndValidators { get; set; } = false;
}