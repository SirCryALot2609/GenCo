using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Features.Relations.Commands.DeleteRelation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.ResoreRelation
{
    public class ResoreRelationCommandHandler(
        IRelationRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<ResoreRelationCommand, BaseResponseDto<RelationBaseDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<RelationBaseDto>> Handle(ResoreRelationCommand request, CancellationToken cancellationToken)
        {
            var relation = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (relation == null)
                return BaseResponseDto<RelationBaseDto>.Fail("Relation not found");
            await _repository.RestoreAsync(relation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<RelationBaseDto>(relation);
            return BaseResponseDto<RelationBaseDto>.Ok(dto, "Relation restored successfully");
        }
    }
}
