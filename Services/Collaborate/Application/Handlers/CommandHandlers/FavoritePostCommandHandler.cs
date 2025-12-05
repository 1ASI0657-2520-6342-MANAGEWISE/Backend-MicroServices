using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For IFavoritePostRepository
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class FavoritePostCommandHandler : IRequestHandler<FavoritePostCommand, bool>
{
    private readonly IFavoritePostRepository _favoritePostRepository;

    public FavoritePostCommandHandler(IFavoritePostRepository favoritePostRepository)
    {
        _favoritePostRepository = favoritePostRepository;
    }

    public async Task<bool> Handle(FavoritePostCommand request, CancellationToken cancellationToken)
    {
        var existingFavorite = await _favoritePostRepository.GetAsync(request.UserId, request.PostId);
        if (existingFavorite != null)
        {
            return false; // Already favorited or handle as per requirements
        }

        var favoritePost = new FavoritePost { UserId = request.UserId, PostId = request.PostId };
        await _favoritePostRepository.AddAsync(favoritePost);
        return true;
    }
}
