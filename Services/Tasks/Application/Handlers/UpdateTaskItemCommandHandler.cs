using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, TaskItemDto?>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemDto?> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (taskItem == null || taskItem.ProjectId != request.ProjectId)
            {
                return null; // Or throw specific exceptions
            }

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.DueDate = request.DueDate;
            taskItem.State = request.State;
            taskItem.AssigneeId = request.AssigneeId;
            // CreatedAt should generally not be updated.

            var success = await _taskItemRepository.UpdateAsync(taskItem, cancellationToken);
            if (!success) return null;

            // Re-fetch or map updated task to DTO
            var updatedTask = await _taskItemRepository.GetByIdAsync(request.Id, cancellationToken);
                 if (updatedTask == null) return null;


            return new TaskItemDto(
                updatedTask.Id,
                updatedTask.Title,
                updatedTask.Description,
                updatedTask.CreatedAt,
                updatedTask.DueDate,
                updatedTask.State,
                updatedTask.AssigneeId,
                "Assignee Name Placeholder", // Placeholder
                "Assignee Image Placeholder", // Placeholder
                updatedTask.ProjectId
            );
        }
    }
}
