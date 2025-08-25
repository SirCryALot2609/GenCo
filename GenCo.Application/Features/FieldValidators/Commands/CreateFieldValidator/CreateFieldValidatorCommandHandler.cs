using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.FieldValidator;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.CreateFieldValidator
{
    public class CreateFieldValidatorCommandHandler(
        IGenericRepository<FieldValidator> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateFieldValidatorCommand, BaseResponseDto<FieldValidatorBaseDto>>
    {
        private readonly IGenericRepository<FieldValidator> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<FieldValidatorBaseDto>> Handle(CreateFieldValidatorCommand request, CancellationToken cancellationToken)
        {
            var fieldValidator = _mapper.Map<FieldValidator>(request.Request);

            fieldValidator.Id = Guid.NewGuid();
            fieldValidator.CreatedAt = DateTime.UtcNow;
            fieldValidator.CreatedBy = "system"; // sau này lấy từ context user

            await _repository.AddAsync(fieldValidator, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FieldValidatorBaseDto>(fieldValidator);

            return BaseResponseDto<FieldValidatorBaseDto>.Ok(dto, "Field Validator created successfully");
        }
    }
}
