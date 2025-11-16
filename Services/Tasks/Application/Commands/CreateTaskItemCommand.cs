using Application.DTOs;
using MediatR;

// For TaskItemDto as response

namespace Application.Commands
{
    public record CreateTaskItemCommand(
        string Title,
        string? Description,
        DateOnly DueDate,
        int ProjectId,
        string State,
        int AssigneeId) : IRequest<TaskItemDto>; // Returns the created TaskItemDto
}
