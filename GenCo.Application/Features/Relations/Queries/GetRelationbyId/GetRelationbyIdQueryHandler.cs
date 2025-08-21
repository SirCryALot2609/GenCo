using AutoMapper;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationbyId
{
    public class GetRelationbyIdQueryHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<GetRelationbyIdQuery, RelationResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<RelationResponseDto> Handle(GetRelationbyIdQuery request, CancellationToken cancellationToken)
        {
            var relation = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<RelationResponseDto>(relation);
        }
    }
}
