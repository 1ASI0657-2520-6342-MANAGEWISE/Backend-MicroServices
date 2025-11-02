using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Handlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResource>
    {
        private readonly IProjectRepository _projectRepository;
        // In a real app, you might inject a service to get user details for TeamMemberDto

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectResource> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                CompanyId = request.CompanyId,
                ProjectDate = DateOnly.Parse(request.ProjectDate), // Add error handling
                ProjectTime = TimeOnly.Parse(request.ProjectTime), // Add error handling
                ProjectLocation = request.ProjectLocation,
                AuditDate = DateOnly.FromDateTime(DateTime.UtcNow), // Or set by DB
                ImageUrls = request.ImageUrl.Select(url => new ProjectImage { Url = url }).ToList()
                // TeamMemberUserIds will be empty on creation, added via AddTeamMemberCommand
            };

            var createdProject = await _projectRepository.AddAsync(project, cancellationToken);

            // Basic mapping to ProjectResource
            return new ProjectResource(
                createdProject.Id,
                createdProject.AuditDate,
                createdProject.Name,
                createdProject.Description,
                createdProject.ProjectDate,
                createdProject.ProjectTime,
                createdProject.ProjectLocation,
                createdProject.CompanyId,
                new List<TeamMemberDto>(), // Team members added separately
                createdProject.ImageUrls.Select(img => img.Url).ToList(),
                createdProject.Rating
            );
        }
    }
}
