using AutoMapper;
using GenCo.Application.DTOs.Entity.Response;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Features.Entities.Queries.GetEntitiesByProjectId;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetAllEntities
{
    public class GetEntitiesByProjectIdQueryHandler(IEntityRepository repository, IMapper mapper)
        : IRequestHandler<GetEntitiesByProjectIdQuery, EntitiesByProjectIdResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<EntitiesByProjectIdResponseDto> Handle(GetEntitiesByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetEntitiesByProjectIdAsync(request.ProjectId);
            if (entities == null || entities.Count == 0)
            {
                return new EntitiesByProjectIdResponseDto
                {
                    Success = false,
                    Message = "No entities found for the given project.",
                    RetrievedAt = DateTime.UtcNow
                };
            }
            var entityDtos = _mapper.Map<List<EntityResponseDto>>(entities);
            return new EntitiesByProjectIdResponseDto
            {
                Success = true,
                Message = "Entities retrieved successfully.",
                RetrievedAt = DateTime.UtcNow,
                Entities = entityDtos
            };
        }
    }
}
