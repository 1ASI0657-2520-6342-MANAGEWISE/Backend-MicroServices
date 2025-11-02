namespace Domain.ValueObjects
{
    public class ProjectImage
    {
        public int Id { get; set; } // If this is a value object, it might not need an Id unless it's also an entity
        public required string Url { get; set; }
        public int ProjectId { get; set; } // Foreign key to Project

        // Parameterless constructor for EF Core
        public ProjectImage() {}
    }
}
