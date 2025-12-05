using MediatR;
using AidManager.Collaborate.Application.DTOs;
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Queries;

public record GetCommentsByPostIdQuery(int PostId) : IRequest<IEnumerable<CommentDto>>;
