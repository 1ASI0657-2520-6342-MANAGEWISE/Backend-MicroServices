using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For IPostRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null)
        {
            return false;
        }
        await _postRepository.DeleteAsync(request.Id);
        return true;
    }
}
