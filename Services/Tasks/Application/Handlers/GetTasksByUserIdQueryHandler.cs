using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetTasksByUserIdQueryHandler : IRequestHandler<GetTasksByUserIdQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetTasksByUserIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);
            return tasks.Select(t => new TaskItemDto(
                t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate, t.State, t.AssigneeId,
                "Assignee Name Placeholder", "Assignee Image Placeholder",
                t.ProjectId
            )).ToList();
        }
    }
}
