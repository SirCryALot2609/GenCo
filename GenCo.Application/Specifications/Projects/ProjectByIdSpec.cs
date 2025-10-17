using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Projects;

public sealed class ProjectByIdSpec : BaseSpecification<Project>
{
    public ProjectByIdSpec(
        Guid projectId,
        bool includeEntities = false,
        bool includeFields = false,
        bool includeValidators = false,
        bool includeRelations = false,
        bool includeWorkflows = false,
        bool includeUiConfigs = false,
        bool includeServiceConfigs = false,
        bool includeConnections = false)
        : base(p => p.Id == projectId)
    {
        if (includeEntities)
        {
            AddInclude(p => p.Entities);

            if (includeFields)
                AddInclude(p => p.Entities.Select(e => e.Fields));

            if (includeValidators)
                AddInclude(p => p.Entities.SelectMany(e => e.Fields)
                    .SelectMany(f => f.Validators));
        }

        if (includeRelations)
            AddInclude(p => p.Relations);

        if (includeWorkflows)
            AddInclude(p => p.Workflows);

        if (includeUiConfigs)
            AddInclude(p => p.UiConfigs);

        if (includeServiceConfigs)
            AddInclude(p => p.ServiceConfigs);

        if (includeConnections)
            AddInclude(p => p.Connections);
    }
}