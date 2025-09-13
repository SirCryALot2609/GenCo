using System.Linq.Expressions;

namespace GenCo.Application.Specifications.Common;

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