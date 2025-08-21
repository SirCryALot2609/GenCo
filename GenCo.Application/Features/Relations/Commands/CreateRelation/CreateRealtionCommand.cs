using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation
{
    public class CreateRealtionCommand : IRequest<BaseCreateResponseDto>
    {
        public CreateRelationRequestDto Request { get; set; } = default!;
    }
}
