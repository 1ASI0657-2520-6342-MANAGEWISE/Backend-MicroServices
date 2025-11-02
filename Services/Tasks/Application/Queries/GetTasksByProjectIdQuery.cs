using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetTasksByProjectIdQuery(int ProjectId) : IRequest<IEnumerable<TaskItemDto>>;
}
