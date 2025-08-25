using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation
{
    public class CreateRelationCommandHandler(
        IRelationRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<CreateRealtionCommand, BaseResponseDto<RelationBaseDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<RelationBaseDto>> Handle(CreateRealtionCommand request, CancellationToken cancellationToken)
        {
            var relation = _mapper.Map<Relation>(request.Request);

            relation.Id = Guid.NewGuid();
            relation.CreatedAt = DateTime.UtcNow;
            relation.CreatedBy = "system"; // sau này lấy từ context user

            await _repository.AddAsync(relation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<RelationBaseDto>(relation);

            return BaseResponseDto<RelationBaseDto>.Ok(dto, "Relation created successfully");
        }
    }
}
