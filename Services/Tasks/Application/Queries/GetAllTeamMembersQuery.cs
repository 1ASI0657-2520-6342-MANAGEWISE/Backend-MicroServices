using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    // Changed name from GetAllTeamMembers to GetAllTeamMembersByProjectIdQuery for clarity
    public record GetAllTeamMembersByProjectIdQuery(int ProjectId) : IRequest<IEnumerable<TeamMemberDto>>;
}
