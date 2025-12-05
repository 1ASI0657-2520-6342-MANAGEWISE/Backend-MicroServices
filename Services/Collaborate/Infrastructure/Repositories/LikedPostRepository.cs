using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class LikedPostRepository : ILikedPostRepository
{
    private readonly CollaborateDbContext _context;

    public LikedPostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LikedPost likedPost)
    {
        await _context.LikedPosts.AddAsync(likedPost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int postId)
    {
        var likedPost = await _context.LikedPosts
            .FirstOrDefaultAsync(lp => lp.UserId == userId && lp.PostId == postId);
        if (likedPost != null)
        {
            _context.LikedPosts.Remove(likedPost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<LikedPost?> GetAsync(int userId, int postId)
    {
        return await _context.LikedPosts
            .FirstOrDefaultAsync(lp => lp.UserId == userId && lp.PostId == postId);
    }

    public async Task<IEnumerable<Post>> GetLikedPostsByUserIdAsync(int userId)
    {
        return await _context.LikedPosts
            .Where(lp => lp.UserId == userId)
            .Include(lp => lp.Post)
                .ThenInclude(p => p!.PostImages)
            .Select(lp => lp.Post!)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetLikesCountAsync(int postId)
    {
        return await _context.LikedPosts.CountAsync(lp => lp.PostId == postId);
    }
}
