using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationById
{
    public record GetRelationByIdQuery(Guid RelationId) : IRequest<BaseResponseDto<RelationBaseDto>>
    {
    }
}
