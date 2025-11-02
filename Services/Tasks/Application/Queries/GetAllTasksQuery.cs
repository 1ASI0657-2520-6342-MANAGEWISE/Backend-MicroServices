using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetAllTasksQuery(int CompanyId) : IRequest<IEnumerable<TaskItemDto>>;
}
