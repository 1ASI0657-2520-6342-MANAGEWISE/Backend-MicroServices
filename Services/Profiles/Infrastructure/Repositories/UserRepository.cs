using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProfilesDbContext _context;

        public UserRepository(ProfilesDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }
    }
}
