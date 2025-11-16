using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetAllProjectsByUserIdQuery(int UserId) : IRequest<IEnumerable<ProjectResource>>;
}
