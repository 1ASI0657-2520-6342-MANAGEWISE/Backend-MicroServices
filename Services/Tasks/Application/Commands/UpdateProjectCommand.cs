using Application.DTOs;
using MediatR;

// For ProjectResource as response

namespace Application.Commands
{
    public record UpdateProjectCommand(
        int ProjectId,
        string Name,
        string? Description,
        List<string> ImageUrl,
        int CompanyId,
        string ProjectDate, // Keep as string
        string ProjectTime, // Keep as string
        string ProjectLocation) : IRequest<ProjectResource?>; // Returns updated ProjectResource or null if not found
}
