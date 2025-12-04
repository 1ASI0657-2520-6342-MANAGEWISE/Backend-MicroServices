using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For ICommentRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        // In a real scenario, you might want to check if the comment belongs to the post (request.PostId)
        // or if the user has permission to delete.
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null || comment.PostId != request.PostId)
        {
            return false; // Or throw an exception
        }
        await _commentRepository.DeleteAsync(request.CommentId);
        return true; // Assuming delete is successful if no exception
    }
}
