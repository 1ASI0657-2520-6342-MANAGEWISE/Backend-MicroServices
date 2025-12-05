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
    private readonly IProfilesClient _profilesClient;

    public GetAllPostsByCompanyIdQueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IProfilesClient profilesClient)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _profilesClient = profilesClient;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetByCompanyIdAsync(request.CompanyId);
        var postDtos = new List<PostDto>();

        var userCache = new Dictionary<int, ProfilesUserDto?>();

        foreach (var post in posts)
        {
            var comments = await _commentRepository.GetByPostIdAsync(post.Id);
            var commentDtos = comments.Select(c => new CommentDto(
                c.Id,
                c.Content,
                c.UserId,
                "User",            
                "Email",
                "Image",
                c.PostId,
                c.TimeOfComment
            )).ToList();

            if (!userCache.TryGetValue(post.UserId, out var user))
            {
                user = await _profilesClient.GetUserByIdAsync(post.UserId, cancellationToken);
                userCache[post.UserId] = user;
            }

            var userName  = user?.Name       ?? "AuthorName";
            var userImage = user?.ProfileImg ?? "AuthorImage";
            var email     = user?.Email      ?? "author@example.com";

            postDtos.Add(new PostDto(
                post.Id,
                post.Title,
                post.Subject,
                post.Description,
                post.CreatedAt,
                post.CompanyId,
                post.UserId,
                userName,
                userImage,
                email,
                post.Rating,
                post.PostImages.Select(img => img.ImageUrl).ToList(),
                commentDtos
            ));
        }

        return postDtos;
    }
}