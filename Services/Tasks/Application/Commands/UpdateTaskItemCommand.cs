using Application.DTOs;
using MediatR;

// For TaskItemDto as response

namespace Application.Commands
{
    public record UpdateTaskItemCommand(
        int Id,
        string Title,
        string? Description,
        DateOnly DueDate,
        string State,
        int AssigneeId,
        int ProjectId) : IRequest<TaskItemDto?>; // Returns updated TaskItemDto or null
}
