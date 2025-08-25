using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Features.FieldValidators.Commands.SoftDeleteFieldValidator;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.UpdateFieldValidator
{
    public class UpdateFieldValidatorCommandHandler(
        IGenericRepository<FieldValidator> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateFieldValidatorCommand, BaseResponseDto<FieldValidatorResponseDto>>
    {
        private readonly IGenericRepository<FieldValidator> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseResponseDto<FieldValidatorResponseDto>> Handle(UpdateFieldValidatorCommand request, CancellationToken cancellationToken)
        {
            var fieldValidator = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (fieldValidator == null)
            {
                return BaseResponseDto<FieldValidatorResponseDto>.Fail("Field not found");
            }

            // Map từ request sang entity
            _mapper.Map(request.Request, fieldValidator);

            fieldValidator.UpdatedAt = DateTime.UtcNow;
            fieldValidator.UpdatedBy = "system"; // sau này lấy từ context user

            await _repository.UpdateAsync(fieldValidator, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FieldValidatorResponseDto>(fieldValidator);

            return BaseResponseDto<FieldValidatorResponseDto>.Ok(dto, "Field updated successfully");
        }
    }
}
