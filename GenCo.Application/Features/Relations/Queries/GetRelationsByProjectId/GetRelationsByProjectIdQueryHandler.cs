using AutoMapper;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationsByProjectId
{
    public class GetRelationsByProjectIdQueryHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<GetRelationsByProjectIdQuery, RelationsByProjectIdResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<RelationsByProjectIdResponseDto> Handle(GetRelationsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var relations = await _repository.GetRelationsByProjectIdAsync(request.ProjectId);
            if (relations == null || relations.Count == 0)
            {
                return new RelationsByProjectIdResponseDto
                {
                    Success = false,
                    Message = "No relations found for the given project.",
                    RetrievedAt = DateTime.UtcNow
                };
            }
            var relationDtos = _mapper.Map<List<RelationResponseDto>>(relations);
            return new RelationsByProjectIdResponseDto
            {
                Success = true,
                Message = "Relations retrieved successfully.",
                RetrievedAt = DateTime.UtcNow,
                Relations = relationDtos
            };
        }
    }
}
