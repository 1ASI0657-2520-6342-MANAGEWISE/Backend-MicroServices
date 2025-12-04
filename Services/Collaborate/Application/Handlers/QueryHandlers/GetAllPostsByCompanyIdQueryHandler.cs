using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetAllPostsByCompanyIdQueryHandler : IRequestHandler<GetAllPostsByCompanyIdQuery, IEnumerable<PostDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository;

    public GetAllPostsByCompanyIdQueryHandler(IPostRepository postRepository, ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetByCompanyIdAsync(request.CompanyId);
        var postDtos = new List<PostDto>();
        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetByPostIdAsync(post.Id);
            var commentDtos = comments.Select(c => new CommentDto(
                c.Id, c.Content, c.UserId, "User", "Email", "Image", c.PostId, c.TimeOfComment)).ToList();
            // var author = await _userRepository.GetByIdAsync(post.UserId);
            postDtos.Add(new PostDto(
                post.Id, post.Title, post.Subject, post.Description, post.CreatedAt,
                post.CompanyId, post.UserId, "AuthorName", "AuthorImage", "author@example.com", post.Rating,
                post.PostImages.Select(img => img.ImageUrl).ToList(), commentDtos));
        }
        return postDtos;
    }
}
