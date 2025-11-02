using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        // May need a user service to get AssigneeName and AssigneeImage

        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemDto> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                ProjectId = request.ProjectId,
                State = request.State, // Consider validating state
                AssigneeId = request.AssigneeId,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow) // Or set by DB
            };

            var createdTask = await _taskItemRepository.AddAsync(taskItem, cancellationToken);

            // Basic mapping (AssigneeName, AssigneeImage are placeholders)
            return new TaskItemDto(
                createdTask.Id,
                createdTask.Title,
                createdTask.Description,
                createdTask.CreatedAt,
                createdTask.DueDate,
                createdTask.State,
                createdTask.AssigneeId,
                "Assignee Name Placeholder", // Placeholder
                "Assignee Image Placeholder", // Placeholder
                createdTask.ProjectId
            );
        }
    }
}
