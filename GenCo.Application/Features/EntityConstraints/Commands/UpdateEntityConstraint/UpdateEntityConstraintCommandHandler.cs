using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Response;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.UpdateEntityConstraint;


public class UpdateEntityConstraintCommandHandler(
    IGenericRepository<EntityConstraint> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateEntityConstraintCommand, BaseResponseDto<EntityConstraintResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintResponseDto>> Handle(UpdateEntityConstraintCommand request, CancellationToken cancellationToken)
    {
        var constraint = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (constraint == null)
            return BaseResponseDto<EntityConstraintResponseDto>.Fail("EntityConstraint not found");

        mapper.Map(request.Request, constraint);
        constraint.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(constraint, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityConstraintResponseDto>(constraint);
        return BaseResponseDto<EntityConstraintResponseDto>.Ok(dto, "EntityConstraint updated successfully");
    }
}