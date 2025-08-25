using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.DTOs.Relation.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation
{
    public record DeleteRelationCommand(DeleteRelationRequestDto Request)
        : IRequest<BaseResponseDto<BoolResultDto>>
    {
    }
}
