using MediatR;
using AidManager.Collaborate.Application.DTOs; // For CommentDto if it's a return type

namespace AidManager.Collaborate.Application.Commands;

// Assuming this command returns the created CommentDto
public record AddCommentCommand(int UserId, string Comment, int PostId) : IRequest<CommentDto>;
