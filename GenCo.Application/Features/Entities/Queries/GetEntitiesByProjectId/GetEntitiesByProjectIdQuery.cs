using GenCo.Application.DTOs.Entity.Response;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntitiesByProjectId
{
    public class GetEntitiesByProjectIdQuery(Guid projectId) : IRequest<EntitiesByProjectIdResponseDto>
    {
        public Guid ProjectId { get; set; } = projectId;
    }
}
