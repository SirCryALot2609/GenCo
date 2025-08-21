using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation
{
    public class DeleteRelationCommandHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<DeleteRelationCommand, BaseUpdateResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var realation = await _repository.GetByIdAsync(request.Request.Id);
            if (realation == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Relation not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            var deleted = await _repository.DeleteAsync(realation);
            return new BaseUpdateResponseDto
            {
                Success = deleted,
                Message = deleted
                    ? "Project deleted successfully."
                    : "Failed to delete project.",
                UpdatedAt = realation.CreatedAt,
                UpdatedBy = realation.UpdateBy,
            };
        }
    }
}
