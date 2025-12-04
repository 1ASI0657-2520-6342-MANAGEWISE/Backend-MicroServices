namespace AidManager.Collaborate.Application.DTOs;

// Forward declaration for CommentResource if it's complex or defined elsewhere.
// For simplicity, if CommentResource is just a string or simple type, define it directly or use a placeholder.
// Assuming CommentResource might be similar to CommentDto for now, or a simplified version.
// If CommentResource is its own DTO, it should be defined as well.
// For now, let's assume PostDto's CommentsList will take CommentDto.

public record CommentDto(
    int Id,
    string Comment,
    int UserId,
    string UserName,    // Consider how to populate this - typically from a User service or denormalized data
    string UserEmail,   // Same as UserName
    string UserImage,   // Same as UserName
    int PostId,
    DateTime CommentTime // Changed from string to DateTime for consistency
);
