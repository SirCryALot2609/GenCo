using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation
{
    public class CreateRelationCommandHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<CreateRealtionCommand, BaseCreateResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateRealtionCommand request, CancellationToken cancellationToken)
        {
            var relation = _mapper.Map<Relation>(request.Request);
            relation.CreatedAt = DateTime.UtcNow;
            relation.CreatedBy = "system"; // TODO: lấy từ context user hiện tại
            var created = await _repository.AddAsync(relation);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "Relation created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
