using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.CreateField
{
    public class CreateFieldCommandHandler(IFieldRepository repository, IMapper mapper)
        : IRequestHandler<CreateFieldCommand, BaseCreateResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
        {
            var field = _mapper.Map<Field>(request.Request);
            field.CreatedAt = DateTime.UtcNow;
            field.CreatedBy = "system"; // TODO: lấy từ context user hiện tại
            var created = await _repository.AddAsync(field);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "Field created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
