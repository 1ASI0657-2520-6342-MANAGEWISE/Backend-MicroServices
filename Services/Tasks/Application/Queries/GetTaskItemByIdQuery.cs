using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetTaskItemByIdQuery(int Id) : IRequest<TaskItemDto?>;
}
