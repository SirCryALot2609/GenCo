using GenCo.Application.DTOs.Relation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationsByProjectId
{
    public class GetRelationsByProjectIdQuery(Guid projectId) : IRequest<RelationsByProjectIdResponseDto>
    {
        public Guid ProjectId { get; set; } = projectId;
    }
}
