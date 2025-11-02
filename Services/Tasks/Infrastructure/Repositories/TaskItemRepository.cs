using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TasksDbContext _context;

        public TaskItemRepository(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(int projectId, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                                 .Include(t => t.Project) // Assuming TaskItem has a navigation property Project
                                 .Where(t => t.Project.CompanyId == companyId)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                                 .Where(t => t.AssigneeId == userId)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAndCompanyIdAsync(int userId, int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                                 .Include(t => t.Project)
                                 .Where(t => t.AssigneeId == userId && t.Project.CompanyId == companyId)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem> AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
        {
            await _context.TaskItems.AddAsync(taskItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return taskItem;
        }

        public async Task<bool> UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
        {
            _context.TaskItems.Update(taskItem);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var taskItem = await _context.TaskItems.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStateAsync(int id, string newState, CancellationToken cancellationToken = default)
        {
            var taskItem = await GetByIdAsync(id, cancellationToken);
            if (taskItem == null) return false;
            taskItem.State = newState; // Assuming State is a string property on TaskItem
            return await UpdateAsync(taskItem, cancellationToken);
        }

        // The following methods were from the old concrete implementation but are not in ITaskItemRepository
        // Or their signatures differ. For now, they are removed to ensure interface compliance.
        // If they are needed, they should be added to the interface first or used internally.
        // public async Task<IEnumerable<TaskItem>> GetAllAsync()
        // {
        //     return await _context.TaskItems.ToListAsync();
        // }
        // public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId)
        // {
        //     return await _context.TaskItems
        //         .Where(t => t.ProjectId == projectId)
        //         .ToListAsync();
        // }
    }
}
