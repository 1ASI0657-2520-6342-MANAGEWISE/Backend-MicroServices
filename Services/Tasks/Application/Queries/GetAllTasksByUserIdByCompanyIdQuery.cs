using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetAllTasksByUserIdByCompanyIdQuery(int UserId, int CompanyId) : IRequest<IEnumerable<TaskItemDto>>;
}
