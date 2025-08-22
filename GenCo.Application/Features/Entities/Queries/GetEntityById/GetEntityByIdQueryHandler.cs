using AutoMapper;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntityById
{
    public class GetEntityByIdQueryHandler(IEntityRepository repository, IMapper mapper)
        : IRequestHandler<GetEntityByIdQuery, EntityDetailsResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<EntityDetailsResponseDto> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            return entity == null ? throw new KeyNotFoundException("Entity not found") 
                : _mapper.Map<EntityDetailsResponseDto>(entity);
        }
    }
}
