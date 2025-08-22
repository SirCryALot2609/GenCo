using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Common
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
        IReadOnlyList<string> IncludeStrings { get; }
        IReadOnlyList<Expression<Func<T, object>>> OrderBys { get; }
        IReadOnlyList<Expression<Func<T, object>>> OrderByDescendings { get; }
        int? Take { get; }
        int? Skip { get; }
        bool IsPagingEnabled { get; }
        bool IsNoTracking { get; }
    }

}
