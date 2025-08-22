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
        public ProjectByKeywordSpec(string keyword, int skip = 0, int take = 10)
            : base(p => !string.IsNullOrEmpty(keyword) &&
                        (p.Name.Contains(keyword) || (p.Description != null && p.Description.Contains(keyword))))
        {
            ApplyOrderBy(p => p.Name);
            ApplyPaging(skip, take);
        }
    }
}
