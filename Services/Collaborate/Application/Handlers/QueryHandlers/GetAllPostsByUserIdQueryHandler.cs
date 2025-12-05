using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetAllPostsByUserIdQueryHandler : IRequestHandler<GetAllPostsByUserIdQuery, IEnumerable<PostDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository;

    public GetAllPostsByUserIdQueryHandler(IPostRepository postRepository, ICommentRepository commentRepository/*, IUserRepository userRepository*/)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetByUserIdAsync(request.UserId);
        var postDtos = new List<PostDto>();
        // var author = await _userRepository.GetByIdAsync(request.UserId); // Assuming all posts are by this user

        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetByPostIdAsync(post.Id);
            var commentDtos = comments.Select(c => new CommentDto(
                c.Id, c.Content, c.UserId, "CommenterName", "CommenterEmail", "CommenterImage", c.PostId, c.TimeOfComment)).ToList();

            postDtos.Add(new PostDto(
                post.Id, post.Title, post.Subject, post.Description, post.CreatedAt,
                post.CompanyId, post.UserId,
                "AuthorNamePlaceholder", // author?.Username ?? "N/A",
                "AuthorImage.png", // author?.ProfileImageUrl ?? "N/A",
                "author@example.com", // author?.Email ?? "N/A",
                post.Rating,
                post.PostImages.Select(img => img.ImageUrl).ToList(), commentDtos));
        }
        return postDtos;
    }
}
