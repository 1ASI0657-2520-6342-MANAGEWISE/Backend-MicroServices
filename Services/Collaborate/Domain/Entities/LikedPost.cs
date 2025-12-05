namespace AidManager.Collaborate.Domain.Entities;

public class LikedPost
{
    public int UserId { get; set; } // Assuming UserId links to a User entity
    public int PostId { get; set; }

    public virtual Post? Post { get; set; } // Navigation property
    // If you have a User entity in Collaborate, add: public virtual User User { get; set; }
}
