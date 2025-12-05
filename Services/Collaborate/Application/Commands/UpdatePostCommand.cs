using MediatR;
using AidManager.Collaborate.Application.DTOs; // For PostDto
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Commands;

// Assuming this command returns the updated PostDto
public record UpdatePostCommand(
    int Id,
    string Title,
    string Subject,
    string Description,
    int CompanyId, // Assuming CompanyId can be updated
    // UserId is typically not updated for a post
    List<string> Images // To update/replace images
) : IRequest<PostDto?>;
