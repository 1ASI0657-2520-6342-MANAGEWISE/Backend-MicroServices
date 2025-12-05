namespace AidManager.Collaborate.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; } // Assuming UserId links to a User entity (perhaps in IAM)
    public string Content { get; set; } = string.Empty;
    public int PostId { get; set; }
    public DateTime TimeOfComment { get; set; }

    public virtual Post? Post { get; set; } // Navigation property
}
