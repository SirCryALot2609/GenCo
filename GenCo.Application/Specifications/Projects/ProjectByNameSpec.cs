using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Projects;

public class ProjectByNameSpec : BaseSpecification<Project>
{
    public ProjectByNameSpec(string name, bool includeEntities = false)
        : base(p => p.Name == name)
    {
        if (!includeEntities) return;
        AddInclude(p => p.Entities);
        AddInclude(p => p.Relations);
        AddInclude(p => p.Workflows);
        AddInclude(p => p.UiConfigs);
        AddInclude(p => p.ServiceConfigs);
        AddInclude(p => p.Connections);
    }
}