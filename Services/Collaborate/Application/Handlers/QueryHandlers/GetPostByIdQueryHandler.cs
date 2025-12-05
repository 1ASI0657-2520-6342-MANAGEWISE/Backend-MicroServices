using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IProfilesClient _profilesClient;

    public GetPostByIdQueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IProfilesClient profilesClient)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _profilesClient = profilesClient;
    }

    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null) return null;

        // 🔹 Comentarios (por ahora con placeholders de usuario)
        var comments = await _commentRepository.GetByPostIdAsync(post.Id);
        var commentDtos = comments.Select(c => new CommentDto(
            c.Id,
            c.Content,
            c.UserId,
            "UserNamePlaceholder",     // TODO: si quieres, también puedes llamar a Profiles aquí
            "user@example.com",
            "userimage.png",
            c.PostId,
            c.TimeOfComment
        )).ToList();

        // 🔹 Datos del autor desde Profiles
        var user = await _profilesClient.GetUserByIdAsync(post.UserId, cancellationToken);

        var userName  = user?.Name       ?? "AuthorName";
        var userImage = user?.ProfileImg ?? "AuthorImage.png";
        var email     = user?.Email      ?? "author@example.com";

        return new PostDto(
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
        );
    }
}