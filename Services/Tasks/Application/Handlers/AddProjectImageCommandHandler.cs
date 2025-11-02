using Application.Commands;
using Application.Interfaces;
using Domain.ValueObjects;
using MediatR;

// For ProjectImage

namespace Application.Handlers
{
    public class AddProjectImageCommandHandler : IRequestHandler<AddProjectImageCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public AddProjectImageCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project == null) return false; // Or throw NotFoundException

            var projectImages = request.ImagesUrl.Select(url => new ProjectImage { Url = url, ProjectId = request.ProjectId }).ToList();
            await _projectRepository.AddProjectImagesAsync(request.ProjectId, projectImages, cancellationToken);
            // Assuming AddProjectImagesAsync handles saving changes or the unit of work does.
            // For now, direct call and assume success if no exception.
            return true;
        }
    }
}
