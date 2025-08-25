using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities.Common; // để enforce BaseEntity

namespace GenCo.Application.Persistence.Contracts.Common
{
    // Repository base cho các entity có BaseEntity (có Id, CreatedAt, IsDeleted...)
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // ======== Query ========

        Task<T?> GetByIdAsync(
            Guid id,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<T>> GetAllAsync(
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        // Query theo specification (chuẩn enterprise)
        Task<IReadOnlyCollection<T>> FindAsync(
            ISpecification<T> specification,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyCollection<T> Items, int TotalCount)> GetPagedAsync(
            ISpecification<T> specification,
            int pageNumber,
            int pageSize,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        // ======== Command ========

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> AddRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<int> DeleteRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        // ======== Soft Delete & Restore ========

        Task<int> SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<int> SoftDeleteRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        Task<int> RestoreAsync(T entity, CancellationToken cancellationToken = default);
    }


    // UnitOfWork cho enterprise
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
