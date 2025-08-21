using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Persistence.Contracts
{
    public interface IWorkFlowRepository : IGenericRepository<Workflow>
    {
        Task<IReadOnlyCollection<Workflow>> GetWorkflowsByProjectIdAsync(Guid projectId);
        Task<Workflow?> GetWorkflowWithStepsAsync(Guid workflowId);
        Task DeleteByProjectIdAsync(Guid projectId);
    }
}
