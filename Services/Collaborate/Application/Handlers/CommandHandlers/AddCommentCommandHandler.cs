using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For ICommentRepository, IUserRepository (if needed for user data)
using AidManager.Collaborate.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // Assuming you might need to fetch user details

    public AddCommentCommandHandler(ICommentRepository commentRepository /*, IUserRepository userRepository */)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<CommentDto> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            UserId = request.UserId,
            Content = request.Comment,
            PostId = request.PostId,
            TimeOfComment = DateTime.UtcNow
        };

        var createdComment = await _commentRepository.AddAsync(comment);

        // Normally, you'd fetch user details here to populate UserName, UserEmail, UserImage
        // For now, returning placeholders or what's available.
        return new CommentDto(
            createdComment.Id,
            createdComment.Content,
            createdComment.UserId,
            "UserNamePlaceholder", // Placeholder
            "user@example.com",    // Placeholder
            "userimage.png",       // Placeholder
            createdComment.PostId,
            createdComment.TimeOfComment
        );
    }
}
