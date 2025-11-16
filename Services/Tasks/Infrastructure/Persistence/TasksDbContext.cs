using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public DbSet<FavoriteProject> FavoriteProjects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project Entity Configuration
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.ProjectLocation).IsRequired().HasMaxLength(200);

                // Relationship: Project has many ProjectImages
                entity.HasMany(e => e.ImageUrls)
                      .WithOne() // Assuming ProjectImage doesn't have a navigation property back to Project
                      .HasForeignKey(pi => pi.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete images if project is deleted

                // TeamMemberUserIds is a List<int>, not directly a navigation property for EF Core to manage as a separate table.
                // This would typically be stored as a JSON string or handled via a linking table if it were more complex.
                // For now, EF Core might try to serialize it if a value converter is set up, or it might be ignored by migrations
                // if not explicitly configured. We'll leave it as is for now and address in repository if conversion is needed.
            });

            // TaskItem Entity Configuration
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.State).IsRequired().HasMaxLength(50); // Consider Enum conversion
                
            });

            // ProjectTeamMember (Join Table for Users and Projects)
            modelBuilder.Entity<ProjectTeamMember>(entity =>
            {
                entity.HasKey(ptm => new { ptm.UserId, ptm.ProjectId }); // Composite key

                // Relationship to Project (assuming Project has a collection of ProjectTeamMembers or handles it indirectly)
                // entity.HasOne<Project>()
                //       .WithMany() // If Project has List<ProjectTeamMember>
                //       .HasForeignKey(ptm => ptm.ProjectId);

                // Relationship to User (UserId is a foreign key to a User table, likely in another DbContext or microservice)
                // No direct navigation here as User entity is not in this Bounded Context.
            });

            // FavoriteProject (Join Table for Users and Projects)
            modelBuilder.Entity<FavoriteProject>(entity =>
            {
                entity.HasKey(fp => new { fp.UserId, fp.ProjectId }); // Composite key
                 // Similar to ProjectTeamMember, relationships to Project and User.
            });

            // ProjectImage Value Object (or Entity)
            modelBuilder.Entity<ProjectImage>(entity =>
            {
                entity.HasKey(pi => pi.Id);
                entity.Property(pi => pi.Url).IsRequired().HasMaxLength(2048);
                // Foreign key to Project is already defined in Project's HasMany relationship.
            });

            // You might want to use .ToTable("TableName") for each entity if you want specific table names.
            // e.g., modelBuilder.Entity<Project>().ToTable("Projects");
        }
    }
}
