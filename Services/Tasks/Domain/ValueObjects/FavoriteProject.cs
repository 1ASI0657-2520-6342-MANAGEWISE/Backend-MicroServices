using Domain.Entities;

namespace Domain.ValueObjects
{
    public class FavoriteProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        // Parameterless constructor for EF Core (if treated as an entity for a join table)
        // Or make it a proper value object (immutable, with overridden Equals and GetHashCode)
        // For now, following the structure provided.
        public FavoriteProject() {}

        public FavoriteProject(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}
