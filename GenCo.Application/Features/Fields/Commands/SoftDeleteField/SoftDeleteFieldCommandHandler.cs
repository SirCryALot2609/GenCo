using GenCo.Application.BusinessRules.Fields;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using MediatR;
using GenCo.Domain.Entities;

namespace GenCo.Application.Features.Fields.Commands.SoftDeleteField;

public class SoftDeleteFieldCommandHandler(
    IGenericRepository<Field> repository,
    IFieldBusinessRules rules,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteFieldCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (field == null)
            return BaseResponseDto<bool>.Fail("Field not found");

        await rules.EnsureFieldCanBeDeletedAsync(field.Id, cancellationToken);

        await repository.SoftDeleteAsync(field, cancellationToken);
        field.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Field soft deleted successfully");
    }
}
