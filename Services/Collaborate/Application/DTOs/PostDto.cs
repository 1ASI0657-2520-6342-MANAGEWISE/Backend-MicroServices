using System; // Required for DateTime
using System.Collections.Generic; // Required for List

namespace AidManager.Collaborate.Application.DTOs;

// This DTO references CommentDto for the comments list.
// The original plan mentioned `List<CommentResource> CommentsList`.
// I'll use `List<CommentDto>` for now, assuming `CommentResource` is represented by `CommentDto`.
// If `CommentResource` is a different structure, it would need its own DTO definition.

public record PostDto(
    int Id,
    string Title,
    string Subject,
    string Description,
    DateTime PostTime,
    int CompanyId,
    int UserId,
    string UserName,    // To be populated (e.g., from User service)
    string UserImage,   // To be populated
    string Email,       // To be populated (User's email)
    int Rating,
    List<string> Images, // List of image URLs
    List<CommentDto> CommentsList // Using CommentDto here
);
