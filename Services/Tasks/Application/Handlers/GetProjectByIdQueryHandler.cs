using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

// Added for List<TeamMemberDto>
// Assuming IUserService or similar for TeamMemberDto population

namespace Application.Handlers
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectResource?>
    {
        private readonly IProjectRepository _projectRepository;
        // private readonly IUserService _userService;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectResource?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
            if (project == null) return null;

            // Placeholder for UserList; requires fetching based on project.TeamMemberUserIds
            var teamMembersDto = new List<TeamMemberDto>();
            // Example: if (project.TeamMemberUserIds.Any()) {
            //    var users = await _userService.GetUsersByIdsAsync(project.TeamMemberUserIds);
            //    teamMembersDto.AddRange(users.Select(u => new TeamMemberDto(u.Id, u.Name, ...)));
            // }

            return new ProjectResource(
                project.Id,
                project.AuditDate,
                project.Name,
                project.Description,
                project.ProjectDate,
                project.ProjectTime,
                project.ProjectLocation,
                project.CompanyId,
                teamMembersDto, // Populated above
                project.ImageUrls.Select(img => img.Url).ToList(),
                project.Rating
            );
        }
    }
}
