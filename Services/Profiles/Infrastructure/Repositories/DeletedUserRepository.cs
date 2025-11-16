using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Infrastructure.Repositories
{
    public class DeletedUserRepository : IDeletedUserRepository
    {
        private readonly ProfilesDbContext _context;

        public DeletedUserRepository(ProfilesDbContext context)
        {
            _context = context;
        }

        public async Task<DeletedUser?> GetByIdAsync(int id)
        {
            return await _context.DeletedUsers.FindAsync(id);
        }

        public async Task<IEnumerable<DeletedUser>> GetAllAsync()
        {
            return await _context.DeletedUsers.ToListAsync();
        }

        public async Task AddAsync(DeletedUser deletedUser)
        {
            await _context.DeletedUsers.AddAsync(deletedUser);
        }
    }
}
