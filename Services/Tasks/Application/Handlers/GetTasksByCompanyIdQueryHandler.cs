using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetTasksByCompanyIdQueryHandler : IRequestHandler<GetTasksByCompanyIdQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetTasksByCompanyIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllByCompanyIdAsync(request.CompanyId, cancellationToken); // Same as GetAllTasksQuery
            return tasks.Select(t => new TaskItemDto(
                t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate, t.State, t.AssigneeId,
                "Assignee Name Placeholder", "Assignee Image Placeholder",
                t.ProjectId
            )).ToList();
        }
    }
}
