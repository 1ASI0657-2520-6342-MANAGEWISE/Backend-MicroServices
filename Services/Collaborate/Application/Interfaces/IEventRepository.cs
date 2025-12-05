using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<IEnumerable<Event>> GetByProjectIdAsync(int projectId);
    Task<Event> AddAsync(Event eventItem);
    Task UpdateAsync(Event eventItem);
    Task DeleteAsync(int id);
}
