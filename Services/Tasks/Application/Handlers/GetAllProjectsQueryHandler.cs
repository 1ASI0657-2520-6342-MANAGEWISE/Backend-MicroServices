using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectResource>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllByCompanyIdAsync(request.CompanyId, cancellationToken);
            return projects.Select(p => new ProjectResource(
                p.Id, p.AuditDate, p.Name, p.Description, p.ProjectDate, p.ProjectTime, p.ProjectLocation, p.CompanyId,
                new List<TeamMemberDto>(), // Placeholder
                p.ImageUrls.Select(img => img.Url).ToList(), p.Rating
            )).ToList();
        }
    }
}
