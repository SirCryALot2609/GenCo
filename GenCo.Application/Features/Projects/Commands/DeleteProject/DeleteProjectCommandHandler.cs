using GenCo.Application.BusinessRules.Projects;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.DeleteProject;
public class DeleteProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork,
    IProjectBusinessRules projectBusinessRules)
    : IRequestHandler<DeleteProjectCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await projectBusinessRules.EnsureProjectExistsAsync(request.Id, cancellationToken);
        await projectBusinessRules.EnsureCanDeleteAsync(request.Id, cancellationToken);
        
        var project = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        await repository.DeleteAsync(project!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Project deleted successfully");
    }
}

