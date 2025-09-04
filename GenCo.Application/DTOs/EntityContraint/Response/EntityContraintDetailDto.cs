namespace GenCo.GenCo.Application.DTOs.EntityContraint.Response;

public class EntityConstraintDetailDto : EntityConstraintBaseDto
{
    public EntityBaseDto Entity { get; set; } = default!;
    public ICollection<EntityConstraintFieldBaseDto> Fields { get; set; } = [];
}
