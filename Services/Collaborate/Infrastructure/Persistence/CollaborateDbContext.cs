using Microsoft.EntityFrameworkCore;
using AidManager.Collaborate.Domain.Entities;

namespace AidManager.Collaborate.Infrastructure.Persistence;

public class CollaborateDbContext : DbContext
{
    public CollaborateDbContext(DbContextOptions<CollaborateDbContext> options)
        : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<FavoritePost> FavoritePosts { get; set; }
    public DbSet<LikedPost> LikedPosts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostImage> PostImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Post entity
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(500);
            entity.Property(e => e.Description).IsRequired();

            entity.HasMany(e => e.PostImages)
                  .WithOne(pi => pi.Post)
                  .HasForeignKey(pi => pi.PostId)
                  .OnDelete(DeleteBehavior.Cascade); // If a post is deleted, its images are also deleted

            entity.HasMany(e => e.Comments)
                  .WithOne(c => c.Post)
                  .HasForeignKey(c => c.PostId)
                  .OnDelete(DeleteBehavior.Cascade); // If a post is deleted, its comments are also deleted
        });

        // Configure Comment entity
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            // Foreign key to Post is already configured by Post's HasMany relationship
        });

        // Configure PostImage entity
        modelBuilder.Entity<PostImage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ImageUrl).IsRequired();
            // Foreign key to Post is already configured by Post's HasMany relationship
        });

        // Configure Event entity
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(300);
        });

        // Configure FavoritePost as a join entity (many-to-many between User and Post)
        modelBuilder.Entity<FavoritePost>(entity =>
        {
            entity.HasKey(fp => new { fp.UserId, fp.PostId }); // Composite key

            entity.HasOne(fp => fp.Post)
                  .WithMany(p => p.FavoritePosts)
                  .HasForeignKey(fp => fp.PostId)
                  .OnDelete(DeleteBehavior.Cascade); // If a post is deleted, favorite entries are deleted

            // Assuming UserId is a foreign key to a User table (potentially external)
            // If User entity was part of this DbContext, you would configure:
            // entity.HasOne(fp => fp.User)
            //       .WithMany(u => u.FavoritePosts) // Assuming User entity has ICollection<FavoritePost>
            //       .HasForeignKey(fp => fp.UserId);
        });

        // Configure LikedPost as a join entity (many-to-many between User and Post)
        modelBuilder.Entity<LikedPost>(entity =>
        {
            entity.HasKey(lp => new { lp.UserId, lp.PostId }); // Composite key

            entity.HasOne(lp => lp.Post)
                  .WithMany(p => p.LikedPosts)
                  .HasForeignKey(lp => lp.PostId)
                  .OnDelete(DeleteBehavior.Cascade); // If a post is deleted, like entries are deleted

            // Assuming UserId is a foreign key to a User table (potentially external)
            // If User entity was part of this DbContext, you would configure:
            // entity.HasOne(lp => lp.User)
            //       .WithMany(u => u.LikedPosts) // Assuming User entity has ICollection<LikedPost>
            //       .HasForeignKey(lp => lp.UserId);
        });
    }
}
