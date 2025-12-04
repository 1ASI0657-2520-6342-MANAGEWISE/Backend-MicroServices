namespace AidManager.Collaborate.Domain.Entities;

public class PostImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int PostId { get; set; }
    public virtual Post? Post { get; set; } // Navigation property
}
