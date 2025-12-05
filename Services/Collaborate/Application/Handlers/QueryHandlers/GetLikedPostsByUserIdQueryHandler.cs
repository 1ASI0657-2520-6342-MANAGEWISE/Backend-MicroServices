using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetLikedPostsByUserIdQueryHandler : IRequestHandler<GetLikedPostsByUserIdQuery, IEnumerable<PostDto>>
{
    private readonly ILikedPostRepository _likedPostRepository;
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository;

    public GetLikedPostsByUserIdQueryHandler(ILikedPostRepository likedPostRepository, ICommentRepository commentRepository/*, IUserRepository userRepository*/)
    {
        _likedPostRepository = likedPostRepository;
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetLikedPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _likedPostRepository.GetLikedPostsByUserIdAsync(request.UserId);
        var postDtos = new List<PostDto>();

        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetByPostIdAsync(post.Id);
            var commentDtos = comments.Select(c => new CommentDto(
                c.Id, c.Content, c.UserId, "Commenter", "Email", "Image", c.PostId, c.TimeOfComment)).ToList();
            // var author = await _userRepository.GetByIdAsync(post.UserId);
            postDtos.Add(new PostDto(
                post.Id, post.Title, post.Subject, post.Description, post.CreatedAt,
                post.CompanyId, post.UserId, "AuthorName", "AuthorImage", "author@example.com", post.Rating,
                post.PostImages.Select(img => img.ImageUrl).ToList(), commentDtos));
        }
        return postDtos;
    }
}
