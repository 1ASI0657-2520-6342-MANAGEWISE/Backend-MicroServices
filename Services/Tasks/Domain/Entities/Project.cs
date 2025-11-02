using Domain.ValueObjects;

// Assuming ProjectImage will be in this namespace

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } // Made nullable as per general practice
        public double Rating { get; set; }
        public List<ProjectImage> ImageUrls { get; set; } = new List<ProjectImage>(); // Changed to List<ProjectImage>
        public List<int> TeamMemberUserIds { get; set; } = new List<int>(); // Changed to List<int>
        public int CompanyId { get; set; }
        public DateOnly ProjectDate { get; set; }
        public TimeOnly ProjectTime { get; set; }
        public required string ProjectLocation { get; set; }
        public DateOnly AuditDate { get; set; } // Consider renaming to CreatedAt or LastModifiedAt for clarity

        // Parameterless constructor for EF Core
        public Project() {}
    }
}
