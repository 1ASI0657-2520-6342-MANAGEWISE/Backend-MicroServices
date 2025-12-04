using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetFavoritePostsByUserIdQueryHandler : IRequestHandler<GetFavoritePostsByUserIdQuery, IEnumerable<PostDto>>
{
    private readonly IFavoritePostRepository _favoritePostRepository;
    private readonly ICommentRepository _commentRepository; // To populate comments in PostDto
    // private readonly IUserRepository _userRepository; // For user details

    public GetFavoritePostsByUserIdQueryHandler(IFavoritePostRepository favoritePostRepository, ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _favoritePostRepository = favoritePostRepository;
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetFavoritePostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _favoritePostRepository.GetFavoritePostsByUserIdAsync(request.UserId);
        var postDtos = new List<PostDto>();

        foreach (var post in posts)
        {
            // Fetch comments for each post
            var comments = await _commentRepository.GetByPostIdAsync(post.Id);
            var commentDtos = comments.Select(c => new CommentDto(
                c.Id, c.Content, c.UserId,
                "UserNamePlaceholder", "user@example.com", "userimage.png", // Placeholders
                c.PostId, c.TimeOfComment)).ToList();

            // Fetch user details for the post author (placeholders)
            // var author = await _userRepository.GetByIdAsync(post.UserId);

            postDtos.Add(new PostDto(
                post.Id,
                post.Title,
                post.Subject,
                post.Description,
                post.CreatedAt,
                post.CompanyId,
                post.UserId,
                "AuthorNamePlaceholder", // author?.Username ?? "N/A",
                "AuthorImage.png", // author?.ProfileImageUrl ?? "N/A",
                "author@example.com", // author?.Email ?? "N/A",
                post.Rating,
                post.PostImages.Select(img => img.ImageUrl).ToList(),
                commentDtos
            ));
        }
        return postDtos;
    }
}
