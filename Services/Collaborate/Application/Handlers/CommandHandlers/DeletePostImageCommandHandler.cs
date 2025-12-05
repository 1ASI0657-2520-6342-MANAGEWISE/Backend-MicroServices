using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For IPostRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class DeletePostImageCommandHandler : IRequestHandler<DeletePostImageCommand, bool>
{
    private readonly IPostRepository _postRepository; // Or a dedicated IPostImageRepository

    public DeletePostImageCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(DeletePostImageCommand request, CancellationToken cancellationToken)
    {
        // Logic to find the post and then the specific image, then delete.
        // This might be better if IPostRepository has a method like GetPostImageByIdAsync
        // For now, using the simplified DeletePostImageAsync from IPostRepository
        await _postRepository.DeletePostImageAsync(request.PostImageId);
        // The success of this operation depends on the repository's implementation.
        // Assuming it throws an exception on failure or the method returns a bool.
        return true; // Placeholder
    }
}
