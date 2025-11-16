using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class UpdateProjectStatusCommandHandler : IRequestHandler<UpdateProjectStatusCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectStatusCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project == null) return false;

            // Logic for updating status based on request.NewStatusId
            // This is a placeholder as the meaning of NewStatusId is not fully defined.
            // E.g., project.Status = (ProjectStatusEnum)request.NewStatusId;
            // For now, assume it's a direct update handled by repository or a specific method.
            // This might require a new method in IProjectRepository like UpdateStatusAsync.
            // For now, returning true as a placeholder.
            // await _projectRepository.UpdateStatusAsync(request.ProjectId, request.NewStatusId, cancellationToken);
            return await Task.FromResult(true); // Placeholder
        }
    }
}
