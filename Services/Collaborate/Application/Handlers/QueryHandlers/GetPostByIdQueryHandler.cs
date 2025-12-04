using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; // For List<CommentDto>

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository; // To fetch comments
    // private readonly IUserRepository _userRepository; // For user details

    public GetPostByIdQueryHandler(IPostRepository postRepository, ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null) return null;

        var comments = await _commentRepository.GetByPostIdAsync(post.Id);
        var commentDtos = comments.Select(c => new CommentDto(
            c.Id, c.Content, c.UserId,
            "UserNamePlaceholder", "user@example.com", "userimage.png", // Placeholders
            c.PostId, c.TimeOfComment)).ToList();

        // Fetch user details (placeholders)
        // var author = await _userRepository.GetByIdAsync(post.UserId);

        return new PostDto(
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
        );
    }
}
