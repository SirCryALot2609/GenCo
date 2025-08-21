using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Persistence.Contracts
{
    public interface IEntityRepository : IGenericRepository<Entity>
    {
        Task<IReadOnlyCollection<Entity>> GetEntitiesByProjectIdAsync(Guid projectId);
        Task<Entity?> GetEntityWithFieldsAsync(Guid entityId);
        Task DeleteByProjectIdAsync(Guid projectId);
        Task<IReadOnlyCollection<Entity>> SearchByNameAsync(Guid projectId, string keyword);
    }
}
