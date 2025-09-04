using GenCo.Application.DTOs.Common;
namespace GenCo.Application.DTOs.Entity.Requests;

public class DeleteEntityRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
}