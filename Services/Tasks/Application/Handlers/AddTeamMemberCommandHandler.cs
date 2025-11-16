using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public AddTeamMemberCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
        {
            // Here, you might also want to check if the user (request.UserId) exists
            // and if the project (request.ProjectId) exists.
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project == null) return false;

            await _projectRepository.AddTeamMemberAsync(request.ProjectId, request.UserId, cancellationToken);
            // Assuming repository handles saving or UoW.
            return true;
        }
    }
}
