using Domain.Entities;
using Microsoft.EntityFrameworkCore;
// For Company entity

// For User entity

namespace Infrastructure.Persistence;

public class IamDbContext : DbContext
{
    public IamDbContext(DbContextOptions<IamDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; } // Added DbSet for Company

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Company entity
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.ManagerId);
            entity.Property(e => e.TeamRegisterCode).IsRequired().HasMaxLength(15);
            entity.HasIndex(e => e.TeamRegisterCode).IsUnique();
        });

        // If you have configurations for User entity, they should remain here
        // Example:
        // modelBuilder.Entity<User>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     // ... other configurations
        // });
    }
}