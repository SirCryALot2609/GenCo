using AutoMapper;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntityById
{
    public class GetEntityWithFieldsQueryHandler(IEntityRepository repository, IMapper mapper) 
        : IRequestHandler<GetEntityWithFieldsQuery, EntityResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<EntityResponseDto> Handle(GetEntityWithFieldsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<EntityResponseDto>(entity);
        }
    }
}
