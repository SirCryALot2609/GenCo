using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Persistence
{
    public interface IConnectionRepository : IGenericRepository<Connection>
    {
        Task<IReadOnlyCollection<Connection>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<Connection?> GetDefaultAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
