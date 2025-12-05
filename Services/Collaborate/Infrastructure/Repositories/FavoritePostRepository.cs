using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class FavoritePostRepository : IFavoritePostRepository
{
    private readonly CollaborateDbContext _context;

    public FavoritePostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FavoritePost favoritePost)
    {
        await _context.FavoritePosts.AddAsync(favoritePost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int postId)
    {
        var favoritePost = await _context.FavoritePosts
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PostId == postId);
        if (favoritePost != null)
        {
            _context.FavoritePosts.Remove(favoritePost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<FavoritePost?> GetAsync(int userId, int postId)
    {
        return await _context.FavoritePosts
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.PostId == postId);
    }

    public async Task<IEnumerable<Post>> GetFavoritePostsByUserIdAsync(int userId)
    {
        return await _context.FavoritePosts
            .Where(fp => fp.UserId == userId)
            .Include(fp => fp.Post) // Include the Post data
                .ThenInclude(p => p!.PostImages) // And its images
            .Select(fp => fp.Post!) // Select the Post entity, ensure it's not null
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}
