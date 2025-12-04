using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For IFavoritePostRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class RemoveFavoritePostCommandHandler : IRequestHandler<RemoveFavoritePostCommand, bool>
{
    private readonly IFavoritePostRepository _favoritePostRepository;

    public RemoveFavoritePostCommandHandler(IFavoritePostRepository favoritePostRepository)
    {
        _favoritePostRepository = favoritePostRepository;
    }

    public async Task<bool> Handle(RemoveFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var existingFavorite = await _favoritePostRepository.GetAsync(request.UserId, request.PostId);
        if (existingFavorite == null)
        {
            return false; // Not favorited or handle as per requirements
        }
        await _favoritePostRepository.DeleteAsync(request.UserId, request.PostId);
        return true;
    }
}
