using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, bool>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public DeleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<bool> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            // The repository's DeleteAsync might need to be updated if it expected ProjectId
            // For now, assuming it only needs the task ID.
            // var task = await _taskItemRepository.GetByIdAsync(request.Id, cancellationToken);
            // if (task == null) return false; // Or throw not found
            return await _taskItemRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
