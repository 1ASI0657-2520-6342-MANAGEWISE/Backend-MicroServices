namespace Domain.Entities
{
    public class ProjectTeamMember
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } // Navigation property

        // Parameterless constructor for EF Core
        public ProjectTeamMember() {}

        public ProjectTeamMember(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}
