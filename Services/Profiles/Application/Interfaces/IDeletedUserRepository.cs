using AidManager.API.Services.Profiles.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Interfaces
{
    public interface IDeletedUserRepository
    {
        Task<DeletedUser?> GetByIdAsync(int id);
        Task<IEnumerable<DeletedUser>> GetAllAsync();
        Task AddAsync(DeletedUser deletedUser);
    }
}
