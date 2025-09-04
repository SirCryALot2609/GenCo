using GenCo.Application.DTOs.Common;

namespace GenCo.GenCo.Application.DTOs.EntityContraint.Requests;

public class DeleteEntityConstraintRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
}

