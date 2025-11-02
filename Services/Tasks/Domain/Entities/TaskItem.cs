namespace Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; } // Made nullable
        public DateOnly DueDate { get; set; }
        public int ProjectId { get; set; }
        public required string State { get; set; } // Consider an Enum for State
        public int AssigneeId { get; set; } // User Id
        public DateOnly CreatedAt { get; set; }
        public Project Project { get; set; } = null!; 

        // Parameterless constructor for EF Core
        public TaskItem() {}
    }
}
