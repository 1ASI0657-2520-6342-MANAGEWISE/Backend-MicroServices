using MediatR;
using AidManager.Collaborate.Application.DTOs; // For PostDto
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Commands;

// Assuming this command returns the created PostDto
public record CreatePostCommand(
    string Title,
    string Subject,
    string Description,
    int CompanyId,
    int UserId,
    List<string> Images
) : IRequest<PostDto>;
