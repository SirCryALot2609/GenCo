using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.RestoreFieldValidator
{
    public class RestoreFieldValidatorCommandHandler(
        IGenericRepository<FieldValidator> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<RestoreFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
    {
        private readonly IGenericRepository<FieldValidator> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(RestoreFieldValidatorCommand request, CancellationToken cancellationToken)
        {
            var fieldValidator = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (fieldValidator == null)
                return BaseResponseDto<FieldValidatorResponseDto>.Fail("Field Validator not found");
            await _repository.RestoreAsync(fieldValidator, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<FieldValidatorResponseDto>(fieldValidator);
            return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "Field Validator restored successfully");
        }
    }
}
