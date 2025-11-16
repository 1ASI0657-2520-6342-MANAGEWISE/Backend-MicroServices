using AidManager.API.Services.Profiles.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
    }
}
