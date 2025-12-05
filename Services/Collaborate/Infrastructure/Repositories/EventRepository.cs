using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly CollaborateDbContext _context;

    public EventRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events.OrderBy(e => e.Date).ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetByProjectIdAsync(int projectId)
    {
        return await _context.Events
            .Where(e => e.ProjectId == projectId)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<Event> AddAsync(Event eventItem)
    {
        await _context.Events.AddAsync(eventItem);
        await _context.SaveChangesAsync();
        return eventItem;
    }

    public async Task UpdateAsync(Event eventItem)
    {
        _context.Events.Update(eventItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventItem = await GetByIdAsync(id);
        if (eventItem != null)
        {
            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
        }
    }
}
