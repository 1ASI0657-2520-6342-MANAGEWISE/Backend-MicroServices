using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Application.Interfaces;

public interface ILikedPostRepository
{
    Task AddAsync(LikedPost likedPost); // For when a user likes a post
    Task DeleteAsync(int userId, int postId); // For when a user unlikes a post
    Task<LikedPost?> GetAsync(int userId, int postId);
    Task<IEnumerable<Post>> GetLikedPostsByUserIdAsync(int userId);
    Task<int> GetLikesCountAsync(int postId); // To get the rating/likes count for a post
}
