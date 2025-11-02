namespace Application.DTOs
{
    public record TaskItemDto(
        int Id,
        string Title,
        string? Description, // Assuming description can be null based on entity
        DateOnly CreatedAt,
        DateOnly DueDate,
        string State,
        int AssigneeId,
        string AssigneeName, // This implies fetching Assignee details
        string AssigneeImage, // This implies fetching Assignee details
        int ProjectId
    );
}
