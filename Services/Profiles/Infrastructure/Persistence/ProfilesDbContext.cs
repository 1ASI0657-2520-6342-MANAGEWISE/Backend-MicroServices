using AidManager.API.Services.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AidManager.API.Services.Profiles.Infrastructure.Persistence
{
    public class ProfilesDbContext : DbContext
    {
        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DeletedUser> DeletedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<DeletedUser>().ToTable("DeletedUsers");
        }
    }
}