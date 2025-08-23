using GenCo.Application.Specifications.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Projects
{
    public class ProjectByKeywordSpec : BaseSpecification<Project>
    {
        public ProjectByKeywordSpec(string? keyword = null, int skip = 0, int take = 10, bool includeAllCollections = false)
            : base(p => string.IsNullOrEmpty(keyword)
                        || p.Name.Contains(keyword)
                        || (p.Description != null && p.Description.Contains(keyword)))
        {
            // Paging
            ApplyPaging(skip, take);

            // Sort
            ApplyOrderBy(p => p.Name);

            // Optional include
            if (includeAllCollections)
            {
                AddInclude(p => p.Entities);
                AddInclude(p => p.Entities.Select(e => e.Fields));
                AddInclude(p => p.Entities.SelectMany(e => e.Fields).Select(f => f.Validators));
                AddInclude(p => p.Relations);
                AddInclude(p => p.Workflows);
                AddInclude(p => p.UIConfigs);
                AddInclude(p => p.ServiceConfigs);
            }
        }
    }

}
