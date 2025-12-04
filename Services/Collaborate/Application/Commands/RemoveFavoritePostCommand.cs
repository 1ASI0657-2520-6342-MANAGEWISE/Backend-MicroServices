using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record RemoveFavoritePostCommand(int PostId, int UserId) : IRequest<bool>;
