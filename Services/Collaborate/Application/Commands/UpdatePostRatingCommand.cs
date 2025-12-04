using MediatR;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Commands;

// This command implies a user is liking/unliking a post, which affects its rating.
// It might be better named LikePostCommand or ToggleLikePostCommand.
// For now, sticking to the name. Assuming it returns the updated PostDto (or just its new rating).
// The original didn't specify a return type, let's assume it returns the updated PostDto.
public record UpdatePostRatingCommand(int PostId, int UserId) : IRequest<PostDto?>;
