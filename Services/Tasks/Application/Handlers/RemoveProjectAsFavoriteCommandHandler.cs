using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class RemoveProjectAsFavoriteCommandHandler : IRequestHandler<RemoveProjectAsFavoriteCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectAsFavoriteCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(RemoveProjectAsFavoriteCommand request, CancellationToken cancellationToken)
        {
            return await _projectRepository.RemoveFavoriteAsync(request.UserId, request.ProjectId, cancellationToken);
        }
    }
}
