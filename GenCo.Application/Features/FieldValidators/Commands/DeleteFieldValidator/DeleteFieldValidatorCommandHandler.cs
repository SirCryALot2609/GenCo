using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator
{
    public class DeleteFieldValidatorCommandHandler(
        IGenericRepository<FieldValidator> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<DeleteFieldValidatorCommand, BaseResponseDto<BoolResultDto>>
    {
        private readonly IGenericRepository<FieldValidator> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<BoolResultDto>> Handle(DeleteFieldValidatorCommand request, CancellationToken cancellationToken)
        {
            var fieldValidator = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (fieldValidator == null)
            {
                return BaseResponseDto<BoolResultDto>.Fail("Field Validator not found");
            }

            await _repository.DeleteAsync(fieldValidator, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponseDto<BoolResultDto>.Ok(new BoolResultDto { Value = true }, "Field Validator deleted successfully");
        }
    }
}
