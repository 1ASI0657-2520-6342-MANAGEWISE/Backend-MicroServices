using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllAsync();
    Task<IEnumerable<Post>> GetByCompanyIdAsync(int companyId);
    Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    // Methods for managing PostImages might also be here or in a dedicated IPostImageRepository
    Task DeletePostImageAsync(int postImageId); // Simplified for now
}
