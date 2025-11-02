using Domain.Entities;

namespace Application.Contracts;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByIdAsync(int id);
    Task AddAsync(User user);

    Task<bool> Update(User user);
    Task<bool> Remove(User user);
    Task<IEnumerable<User>?> ListAsync();
}