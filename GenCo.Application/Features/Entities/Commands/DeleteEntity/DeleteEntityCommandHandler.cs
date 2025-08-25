using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEntityCommand, BaseResponseDto<BoolResultDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponseDto<BoolResultDto>> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return BaseResponseDto<BoolResultDto>.Fail("Entity not found");
            }

            await _repository.DeleteAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponseDto<BoolResultDto>.Ok(new BoolResultDto { Value = true }, "Entity deleted successfully");
        }
    }
}
