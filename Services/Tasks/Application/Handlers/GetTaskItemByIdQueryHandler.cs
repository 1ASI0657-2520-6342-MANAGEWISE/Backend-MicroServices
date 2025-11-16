using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItemDto?>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        // private readonly IUserService _userService; // For assignee details

        public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemDto?> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskItemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (task == null) return null;

            // Placeholder for AssigneeName, AssigneeImage
            return new TaskItemDto(
                task.Id, task.Title, task.Description, task.CreatedAt, task.DueDate, task.State, task.AssigneeId,
                "Assignee Name Placeholder", "Assignee Image Placeholder",
                task.ProjectId
            );
        }
    }
}
