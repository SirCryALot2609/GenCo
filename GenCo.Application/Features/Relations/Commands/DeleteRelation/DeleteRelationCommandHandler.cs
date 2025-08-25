using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation
{
    public class DeleteRelationCommandHandler(
        IRelationRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<DeleteRelationCommand, BaseResponseDto<BoolResultDto>>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<BoolResultDto>> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var relation = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (relation == null)
            {
                return BaseResponseDto<BoolResultDto>.Fail("Relation Validator not found");
            }

            await _repository.DeleteAsync(relation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponseDto<BoolResultDto>.Ok(new BoolResultDto { Value = true }, "Relation deleted successfully");
        }
    }
}
