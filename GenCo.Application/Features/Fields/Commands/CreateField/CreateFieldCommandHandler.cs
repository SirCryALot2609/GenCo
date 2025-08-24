using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.CreateField
{
    public class CreateFieldCommandHandler(
    IGenericRepository<Field> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateFieldCommand, BaseResponseDto<FieldResponseDto>>
    {
        private readonly IGenericRepository<Field> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<FieldResponseDto>> Handle(
            CreateFieldCommand request,
            CancellationToken cancellationToken)
        {
            var field = _mapper.Map<Field>(request.Request);

            field.Id = Guid.NewGuid();
            field.CreatedAt = DateTime.UtcNow;
            field.CreatedBy = "system"; // sau này lấy từ context user

            await _repository.AddAsync(field, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FieldResponseDto>(field);

            return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field created successfully");
        }
    }
}
