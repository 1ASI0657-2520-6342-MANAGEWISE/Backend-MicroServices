using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

// Assuming a way to get TeamMemberDto details

namespace Application.Handlers
{
    public class GetAllProjectsByUserIdQueryHandler : IRequestHandler<GetAllProjectsByUserIdQuery, IEnumerable<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;
        // private readonly IUserService _userService; // Example for fetching user details

        public GetAllProjectsByUserIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectResource>> Handle(GetAllProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);
            // Mapping (simplified for UserList)
            return projects.Select(p => new ProjectResource(
                p.Id, p.AuditDate, p.Name, p.Description, p.ProjectDate, p.ProjectTime, p.ProjectLocation, p.CompanyId,
                new List<TeamMemberDto>(), // Placeholder for actual team member fetching
                p.ImageUrls.Select(img => img.Url).ToList(), p.Rating
            )).ToList();
        }
    }
}
