using GenCo.Application.DTOs.Relation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationbyId
{
    public class GetRelationbyIdQuery(Guid Id) : IRequest<RelationResponseDto>
    {
        public Guid Id { get; set; } = default!;
    }
}
