using GenCo.Application.DTOs.Field.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldsByEntityId
{
    public class GetFieldsByEntityIdQuery(Guid entityId) : IRequest<FieldsByEntityIdResponseDto>
    {
        public Guid EntityId { get; set; } = entityId;
    }
}
