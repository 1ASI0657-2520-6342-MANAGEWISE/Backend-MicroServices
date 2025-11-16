using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetTasksByProjectIdQueryHandler : IRequestHandler<GetTasksByProjectIdQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetTasksByProjectIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllByProjectIdAsync(request.ProjectId, cancellationToken);
            return tasks.Select(t => new TaskItemDto(
                t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate, t.State, t.AssigneeId,
                "Assignee Name Placeholder", "Assignee Image Placeholder",
                t.ProjectId
            )).ToList();
        }
    }
}
