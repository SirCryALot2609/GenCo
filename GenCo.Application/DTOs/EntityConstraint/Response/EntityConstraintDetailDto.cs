using GenCo.Application.DTOs.EntityConstraintField;

namespace GenCo.Application.DTOs.EntityConstraint.Response;

public class EntityConstraintDetailDto : EntityConstraintBaseDto
{
    public ICollection<EntityConstraintFieldBaseDto> Fields { get; set; } = [];
}
