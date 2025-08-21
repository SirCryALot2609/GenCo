using GenCo.Application.DTOs.ServiceConfig.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Queries.GetServiceConfigsByProjectId
{
    public class GetServiceConfigsByProjectIdQuery(Guid projectId) : IRequest<ServiceConfigsByProjectIdResponseDto>
    {
        public Guid ProjecId { get; set; } = projectId;
    }
}
