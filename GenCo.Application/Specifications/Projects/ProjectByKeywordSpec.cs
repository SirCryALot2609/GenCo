using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Projects;

public class ProjectByKeywordSpec : BaseSpecification<Project>
{
    public ProjectByKeywordSpec(
        string? keyword = null,
        int skip = 0,
        int take = 10,
        bool includeAllCollections = false)
        : base(p =>
            string.IsNullOrEmpty(keyword)
            || p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            || (p.Description != null && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
        )
    {
        ApplyPaging(Math.Max(skip, 0), Math.Max(take, 1));

        ApplyOrderBy(p => p.Name);

        if (!includeAllCollections) return;
        {
            AddInclude(p => p.Entities);
            AddInclude(p => p.Entities.Select(e => e.Fields));
            AddInclude(p => p.Entities.Select(e => e.Fields.Select(f => f.Validators)));

            AddInclude(p => p.Relations);
            AddInclude(p => p.Workflows);
            AddInclude(p => p.UiConfigs);
            AddInclude(p => p.ServiceConfigs);
            AddInclude(p => p.Connections);
        }
    }
}
