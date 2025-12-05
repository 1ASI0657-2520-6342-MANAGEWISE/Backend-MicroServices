namespace AidManager.Collaborate.Application.DTOs;

public record EventDto(
    int Id,
    string Name,
    DateTime Date, // Changed from string to DateTime
    string Location,
    string Description,
    string Color,
    int ProjectId
);
