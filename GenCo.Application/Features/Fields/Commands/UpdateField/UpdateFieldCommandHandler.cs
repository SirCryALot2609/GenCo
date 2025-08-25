using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
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

namespace GenCo.Application.Features.Fields.Commands.UpdateField
{
    public class UpdateFieldCommandHandler(
        IFieldRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<UpdateFieldCommand, BaseResponseDto<FieldResponseDto>>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<FieldResponseDto>> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (field == null)
            {
                return BaseResponseDto<FieldResponseDto>.Fail("Field not found");
            }
            _mapper.Map(request.Request, field);
            field.UpdatedAt = DateTime.UtcNow;
            field.UpdatedBy = "system"; // sau này lấy từ context user
            await _repository.UpdateAsync(field, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<FieldResponseDto>(field);
            return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field updated successfully");
        }
    }
}
