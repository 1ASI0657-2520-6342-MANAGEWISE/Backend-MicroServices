using MediatR;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Queries;

public record GetCommentByIdQuery(int CommentId) : IRequest<CommentDto?>;
