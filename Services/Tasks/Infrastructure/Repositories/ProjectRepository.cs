using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TasksDbContext _context;

        public ProjectRepository(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Include(p => p.ImageUrls) // Assuming ImageUrls is a navigation property
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetAllByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Include(p => p.ImageUrls)
                .Where(p => p.CompanyId == companyId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            // Query through the ProjectTeamMembers join table
            return await _context.ProjectTeamMembers
                .Where(ptm => ptm.UserId == userId)
                .Include(ptm => ptm.Project) // Assuming ProjectTeamMember has a navigation property to Project
                    .ThenInclude(p => p.ImageUrls) // And Project has ImageUrls
                .Select(ptm => ptm.Project)
                .ToListAsync(cancellationToken);
        }

        public async Task<Project> AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return project;
        }

        public async Task<bool> UpdateAsync(Project project, CancellationToken cancellationToken = default)
        {
            _context.Projects.Update(project);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync(new object[] { projectId }, cancellationToken);
            if (project != null)
            {
                _context.Projects.Remove(project);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }

        public async Task AddProjectImagesAsync(int projectId, IEnumerable<ProjectImage> images, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync(new object[] { projectId }, cancellationToken);
            if (project != null)
            {
                foreach (var image in images)
                {
                    // Assuming ProjectImage has a ProjectId property or is part of project.ImageUrls collection
                    project.ImageUrls.Add(new ProjectImage { Url = image.Url, ProjectId = projectId }); // Ensure ProjectId is set if it's a separate table linked by FK
                }
                // If ImageUrls is a List<ProjectImage> on Project entity, EF Core change tracking should handle it.
                // If ProjectImage is a separate DbSet, you might need: await _context.ProjectImages.AddRangeAsync(images, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task AddTeamMemberAsync(int projectId, int userId, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync(new object[] { projectId }, cancellationToken);
            if (project != null)
            {
                // Assuming TeamMemberUserIds is a List<int> directly on the Project entity
                if (!project.TeamMemberUserIds.Contains(userId))
                {
                    project.TeamMemberUserIds.Add(userId);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                // If ProjectTeamMember is a join entity:
                // var ptm = new ProjectTeamMember { ProjectId = projectId, UserId = userId };
                // await _context.ProjectTeamMembers.AddAsync(ptm, cancellationToken);
                // await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<int>> GetTeamMemberUserIdsAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects
                                .AsNoTracking() // Good practice if only reading this property
                                .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
            return project?.TeamMemberUserIds ?? Enumerable.Empty<int>();
        }

        public async Task SaveFavoriteAsync(FavoriteProject favoriteProject, CancellationToken cancellationToken = default)
        {
            // Assuming FavoriteProjects is a DbSet for a join table
            var existing = await _context.FavoriteProjects.FindAsync(new object[] { favoriteProject.UserId, favoriteProject.ProjectId }, cancellationToken);
            if (existing == null)
            {
                await _context.FavoriteProjects.AddAsync(favoriteProject, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            var favorite = await _context.FavoriteProjects.FindAsync(new object[] { userId, projectId }, cancellationToken);
            if (favorite != null)
            {
                _context.FavoriteProjects.Remove(favorite);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Project>> GetFavoriteProjectsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.FavoriteProjects
                .Where(fp => fp.UserId == userId)
                .Select(fp => fp.Project)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateRatingAsync(int projectId, double rating, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync(new object[] { projectId }, cancellationToken);
            if (project != null)
            {
                project.Rating = rating;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
    }
}
