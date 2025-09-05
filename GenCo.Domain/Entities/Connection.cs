using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class Connection : BaseEntity
{
    public Guid ProjectId { get; set; }  
    public string Name { get; set; } = default!; 
    public string Provider { get; set; } = default!; 
    public string ConnectionString { get; set; } = default!; 
    public bool IsDefault { get; set; } = false; 
    public Project Project { get; set; } = default!;
}