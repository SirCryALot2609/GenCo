using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.DeleteField
{
    public class DeleteFieldCommandHandler(
        IFieldRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<DeleteFieldCommand, BaseResponseDto<BoolResultDto>>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<BoolResultDto>> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (field == null)
            {
                return BaseResponseDto<BoolResultDto>.Fail("Field not found");
            }

            await _repository.DeleteAsync(field, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponseDto<BoolResultDto>.Ok(new BoolResultDto { Value = true }, "Field deleted successfully");
        }
    }
}
