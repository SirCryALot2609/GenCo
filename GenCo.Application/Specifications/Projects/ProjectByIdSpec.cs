using GenCo.Application.Specifications.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Projects
{
    public class ProjectByIdSpec : BaseSpecification<Project>
    {
        public ProjectByIdSpec(Guid projectId, bool includeAllCollections = false)
        : base(p => p.Id == projectId) 
        {
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
