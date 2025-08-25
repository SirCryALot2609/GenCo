using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Features.Relations.Commands.SoftDeleteRelation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.UpdateRelation
{
    public class UpdateRelationCommandHandler(
        IRelationRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<UpdateRelationCommand, BaseResponseDto<RelationBaseDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<RelationBaseDto>> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
        {
            var relation = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (relation == null)
            {
                return BaseResponseDto<RelationBaseDto>.Fail("Ralation not found");
            }

            _mapper.Map(request.Request, relation);
            relation.UpdatedAt = DateTime.UtcNow;
            relation.UpdatedBy = "system"; // sau này lấy từ context user
            await _repository.UpdateAsync(relation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<RelationBaseDto>(relation);
            return BaseResponseDto<RelationBaseDto>.Ok(dto, "Ralation updated successfully");
        }
    }
}
