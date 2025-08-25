using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Persistence.Contracts
{
    public  interface IRelationRepository : IGenericRepository<Relation>
    {
    }
}
