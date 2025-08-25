using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Features.Relations.Commands.SoftDeleteRelation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;
using GenCo.Application.Specifications.Relations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Queries.GetRelationById
{
    public class GetRelationByIdQueryHandler(
        IRelationRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<GetRelationByIdQuery, BaseResponseDto<RelationBaseDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<RelationBaseDto>> Handle(GetRelationByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new RelationByIdSpec(request.RelationId);
            var relation = await _repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

            if (relation is null)
            {
                return new BaseResponseDto<RelationBaseDto>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }

            var dto = _mapper.Map<RelationBaseDto>(relation);
            return BaseResponseDto<RelationBaseDto>.Ok(dto);
        }
    }
}
