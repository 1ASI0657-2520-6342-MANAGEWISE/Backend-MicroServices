using Application.Commands;
using Application.Interfaces;
using Domain.ValueObjects;
using MediatR;

namespace Application.Handlers
{
    public class SaveProjectAsFavoriteCommandHandler : IRequestHandler<SaveProjectAsFavoriteCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public SaveProjectAsFavoriteCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(SaveProjectAsFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = new FavoriteProject(request.UserId, request.ProjectId);
            await _projectRepository.SaveFavoriteAsync(favorite, cancellationToken);
            return true; // Assuming success if no exception
        }
    }
}
