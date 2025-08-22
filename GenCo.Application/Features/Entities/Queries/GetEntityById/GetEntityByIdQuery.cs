using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntityById
{
    public record GetEntityByIdQuery(Guid Id)
        : IRequest<EntityDetailsResponseDto>;
}
