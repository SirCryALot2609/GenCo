using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Common
{
    public abstract class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
    {
        private readonly List<Expression<Func<T, object>>> _includes = [];
        private readonly List<string> _includeStrings = [];
        private readonly List<Expression<Func<T, object>>> _orderBys = [];
        private readonly List<Expression<Func<T, object>>> _orderByDescendings = [];

        public Expression<Func<T, bool>>? Criteria { get; } = criteria;
        public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();
        public IReadOnlyList<string> IncludeStrings => _includeStrings.AsReadOnly();
        public IReadOnlyList<Expression<Func<T, object>>> OrderBys => _orderBys.AsReadOnly();
        public IReadOnlyList<Expression<Func<T, object>>> OrderByDescendings => _orderByDescendings.AsReadOnly();
        public int? Take { get; private set; }
        public int? Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public bool IsNoTracking { get; private set; }

        // ===== Helper methods =====
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => _includes.Add(includeExpression);

        protected void AddInclude(string includeString)
            => _includeStrings.Add(includeString);

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
            => _orderBys.Add(orderByExpression);

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
            => _orderByDescendings.Add(orderByDescExpression);

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void AsNoTracking() => IsNoTracking = true;
    }
}
