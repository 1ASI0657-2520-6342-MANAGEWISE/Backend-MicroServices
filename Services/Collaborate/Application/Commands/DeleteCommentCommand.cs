using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeleteCommentCommand(int PostId, int CommentId) : IRequest<bool>;
