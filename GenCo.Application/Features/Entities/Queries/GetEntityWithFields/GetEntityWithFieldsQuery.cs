using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntityById
{
    public class GetEntityWithFieldsQuery(Guid Id) : IRequest<EntityResponseDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
