using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Features.Relations.Commands.ResoreRelation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.SoftDeleteRelation
{
    public class SoftDeleteRelationCommandHandler(
        IRelationRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<SoftDeleteRelationCommand, BaseResponseDto<RelationBaseDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<RelationBaseDto>> Handle(SoftDeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var relation = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (relation == null)
                return BaseResponseDto<RelationBaseDto>.Fail("Relation not found.");
            await _repository.SoftDeleteAsync(relation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<RelationBaseDto>(relation);
            return BaseResponseDto<RelationBaseDto>.Ok(dto, "Relation soft deleted successfully");
        }
    }
}
