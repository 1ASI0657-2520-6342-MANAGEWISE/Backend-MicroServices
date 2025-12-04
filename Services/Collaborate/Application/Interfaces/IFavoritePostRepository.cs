using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IFavoritePostRepository
{
    Task AddAsync(FavoritePost favoritePost);
    Task DeleteAsync(int userId, int postId);
    Task<FavoritePost?> GetAsync(int userId, int postId);
    Task<IEnumerable<Post>> GetFavoritePostsByUserIdAsync(int userId);
}
