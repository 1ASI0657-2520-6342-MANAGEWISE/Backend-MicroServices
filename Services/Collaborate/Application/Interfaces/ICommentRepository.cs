using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Application.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
    Task<IEnumerable<Comment>> GetByCompanyIdAsync(int companyId); // Assuming comments can be fetched by company through posts
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}
