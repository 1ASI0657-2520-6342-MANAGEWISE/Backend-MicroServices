using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null) return null;

        // Fetch user details (placeholders for now)
        // var user = await _userRepository.GetByIdAsync(comment.UserId);

        return new CommentDto(
            comment.Id,
            comment.Content,
            comment.UserId,
            "UserNamePlaceholder", // user?.Username ?? "N/A",
            "user@example.com",    // user?.Email ?? "N/A",
            "userimage.png",       // user?.ProfileImageUrl ?? "N/A",
            comment.PostId,
            comment.TimeOfComment
        );
    }
}
