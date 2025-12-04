using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly CollaborateDbContext _context;

    public CommentRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
    {
        return await _context.Comments
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.TimeOfComment) // Show newest comments first
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetByCompanyIdAsync(int companyId)
    {
        // This requires joining Comment -> Post -> CompanyId
        return await _context.Comments
            .Include(c => c.Post) // Ensure Post is loaded to check CompanyId
            .Where(c => c.Post != null && c.Post.CompanyId == companyId)
            .OrderByDescending(c => c.TimeOfComment)
            .ToListAsync();
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
