namespace AidManager.Collaborate.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int Rating { get; set; }
    public int CompanyId { get; set; } // Assuming CompanyId links to a Company entity (external or to be defined)
    public int UserId { get; set; }    // Assuming UserId links to a User entity

    public virtual ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<FavoritePost> FavoritePosts { get; set; } = new List<FavoritePost>();
    public virtual ICollection<LikedPost> LikedPosts { get; set; } = new List<LikedPost>();
}
