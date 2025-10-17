using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Projects;

public sealed class ProjectByNameSpec : BaseSpecification<Project>
{
    public ProjectByNameSpec(
        string name,
        Guid? excludeProjectId = null,
        bool includeRelations = false,
        bool includeEntities = false,
        bool includeWorkflows = false,
        bool includeUiConfigs = false,
        bool includeServiceConfigs = false,
        bool includeConnections = false)
        : base(p =>
            p.Name.ToLower() == name.ToLower() &&
            (excludeProjectId == null || p.Id != excludeProjectId))
    {
        if (includeEntities) AddInclude(p => p.Entities);
        if (includeRelations) AddInclude(p => p.Relations);
        if (includeWorkflows) AddInclude(p => p.Workflows);
        if (includeUiConfigs) AddInclude(p => p.UiConfigs);
        if (includeServiceConfigs) AddInclude(p => p.ServiceConfigs);
        if (includeConnections) AddInclude(p => p.Connections);
    }
}