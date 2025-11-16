using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    // Assuming this is by CompanyId as per the controller action "GetAllProjects" which often implies a scope
    // And matching GetAllTasksQuery(int CompanyId)
    public record GetAllProjectsQuery(int CompanyId) : IRequest<IEnumerable<ProjectResource>>;
}
