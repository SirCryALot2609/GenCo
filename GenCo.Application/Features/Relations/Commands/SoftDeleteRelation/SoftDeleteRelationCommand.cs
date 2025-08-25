using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.DTOs.Relation.Requests;
using GenCo.Application.DTOs.Relation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.SoftDeleteRelation
{
    public record SoftDeleteRelationCommand(DeleteRelationRequestDto Request)
        : IRequest<BaseResponseDto<RelationBaseDto>>
    {
    }
}
