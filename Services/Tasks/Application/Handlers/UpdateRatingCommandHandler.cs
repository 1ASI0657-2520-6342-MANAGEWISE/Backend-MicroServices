using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateRatingCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            return await _projectRepository.UpdateRatingAsync(request.ProjectId, request.Rating, cancellationToken);
        }
    }
}
