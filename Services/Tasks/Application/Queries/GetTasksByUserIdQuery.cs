using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetTasksByUserIdQuery(int UserId) : IRequest<IEnumerable<TaskItemDto>>;
}
