using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly CollaborateDbContext _context;

    public PostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.PostImages)
            .Include(p => p.Comments) // Comments might be paginated in a real app
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        // Consider pagination for real-world scenarios
        return await _context.Posts
            .Include(p => p.PostImages)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByCompanyIdAsync(int companyId)
    {
        return await _context.Posts
            .Include(p => p.PostImages)
            .Where(p => p.CompanyId == companyId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        return await _context.Posts
            .Include(p => p.PostImages)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Post> AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        // EF Core's Update can be tricky with related entities if not handled carefully.
        // Ensure that child collections (PostImages, Comments) are managed correctly
        // (e.g., by loading the post and its children, modifying, then saving,
        // or by handling child entity states explicitly if detached).
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await GetByIdAsync(id); // Using GetByIdAsync ensures related entities are loaded if needed for cascade delete setup
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePostImageAsync(int postImageId)
    {
        var image = await _context.PostImages.FindAsync(postImageId);
        if (image != null)
        {
            _context.PostImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
