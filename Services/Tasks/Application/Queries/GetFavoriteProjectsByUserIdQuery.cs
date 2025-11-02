using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetFavoriteProjectsByUserIdQuery(int UserId) : IRequest<IEnumerable<ProjectResource>>;
}
