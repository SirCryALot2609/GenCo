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

namespace GenCo.Application.Features.Relations.Commands.UpdateRelation
{
    public class UpdateRelationCommandHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<UpdateRelationCommand, BaseUpdateResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
        {
            var realtion = await _repository.GetByIdAsync(request.Request.Id);
            if (realtion == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Relation not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            _mapper.Map(request.Request, realtion);
            var updated = await _repository.UpdateAsync(realtion);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "Relation updated successfully.",
                UpdatedAt = updated.UpdateAt,
                UpdatedBy = updated.UpdateBy,
            };
        }
    }
}
