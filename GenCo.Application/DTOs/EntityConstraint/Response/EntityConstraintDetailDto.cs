using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.EntityConstraintField;

namespace GenCo.Application.DTOs.EntityConstraint.Response;

public class EntityConstraintDetailDto : EntityConstraintBaseDto
{
    public EntityBaseDto Entity { get; set; } = default!;
    public ICollection<EntityConstraintFieldBaseDto> Fields { get; set; } = [];
}
