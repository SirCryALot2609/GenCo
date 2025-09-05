using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.DeleteProject;
public class DeleteProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProjectCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (project == null)
            return BaseResponseDto<bool>.Fail("Project not found");

        await repository.DeleteAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Project deleted successfully");
    }
}
