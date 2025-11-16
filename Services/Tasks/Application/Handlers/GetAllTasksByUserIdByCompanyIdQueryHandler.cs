using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;

// Assuming IUserService for Assignee details

namespace Application.Handlers
{
    public class GetAllTasksByUserIdByCompanyIdQueryHandler : IRequestHandler<GetAllTasksByUserIdByCompanyIdQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        // private readonly IUserService _userService;

        public GetAllTasksByUserIdByCompanyIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksByUserIdByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskItemRepository.GetAllByUserIdAndCompanyIdAsync(request.UserId, request.CompanyId, cancellationToken);
            // Mapping (AssigneeName, AssigneeImage are placeholders)
            return tasks.Select(t => new TaskItemDto(
                t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate, t.State, t.AssigneeId,
                "Assignee Name Placeholder", "Assignee Image Placeholder", // Placeholders
                t.ProjectId
            )).ToList();
        }
    }
}
