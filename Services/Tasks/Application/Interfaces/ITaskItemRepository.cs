using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(int projectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetAllByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default); // Requires joining through Project
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default); // Based on AssigneeId
        Task<IEnumerable<TaskItem>> GetAllByUserIdAndCompanyIdAsync(int userId, int companyId, CancellationToken cancellationToken = default); // Based on AssigneeId and joined Project's CompanyId
        Task<TaskItem> AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        // DeleteAsync might also need projectId for security/scoping, will match command for now.
        Task<bool> UpdateStateAsync(int id, string newState, CancellationToken cancellationToken = default);
    }
}
