using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Persistence.Contracts
{
    public interface IFieldValidatorRepository : IGenericRepository<FieldValidator>
    {
        Task<IReadOnlyCollection<FieldValidator>> GetByFieldIdAsync(Guid fieldId);
        Task DeleteByFieldIdAsync(Guid fieldId);
    }
}
