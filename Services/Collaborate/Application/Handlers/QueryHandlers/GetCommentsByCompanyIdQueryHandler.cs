using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetCommentsByCompanyIdQueryHandler : IRequestHandler<GetCommentsByCompanyIdQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository;

    public GetCommentsByCompanyIdQueryHandler(ICommentRepository commentRepository /*, IUserRepository userRepository*/)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByCompanyIdAsync(request.CompanyId);
             return comments.Select(comment => new CommentDto(
                comment.Id,
                comment.Content,
                comment.UserId,
                "UserNamePlaceholder",
                "user@example.com",
                "userimage.png",
                comment.PostId,
                comment.TimeOfComment
            )).ToList();
    }
}
