using MediatR;
using AidManager.Collaborate.Application.DTOs; // For PostDto
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Queries;

public record GetFavoritePostsByUserIdQuery(int UserId) : IRequest<IEnumerable<PostDto>>;
