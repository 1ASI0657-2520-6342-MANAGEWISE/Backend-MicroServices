using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public GetCommentsByPostIdQueryHandler(ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByPostIdAsync(request.PostId);
        // In a real app, you'd fetch user details for each comment efficiently (e.g., batch load)
        return comments.Select(comment => new CommentDto(
            comment.Id,
            comment.Content,
            comment.UserId,
            "UserNamePlaceholder", // Placeholder
            "user@example.com",    // Placeholder
            "userimage.png",       // Placeholder
            comment.PostId,
            comment.TimeOfComment
        )).ToList();
    }
}
