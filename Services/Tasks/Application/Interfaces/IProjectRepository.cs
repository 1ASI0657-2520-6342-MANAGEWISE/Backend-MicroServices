using Domain.Entities;
using Domain.ValueObjects;

// For ProjectImage and FavoriteProject if they are directly handled by this repo

namespace Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetAllByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken = default); // This might involve joins or a different approach depending on how TeamMembers are structured
        Task<Project> AddAsync(Project project, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Project project, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int projectId, CancellationToken cancellationToken = default);

        // Methods for Project Images
        Task AddProjectImagesAsync(int projectId, IEnumerable<ProjectImage> images, CancellationToken cancellationToken = default);

        // Methods for Team Members
        Task AddTeamMemberAsync(int projectId, int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<int>> GetTeamMemberUserIdsAsync(int projectId, CancellationToken cancellationToken = default); // To get User IDs

        // Methods for Favorite Projects
        Task SaveFavoriteAsync(FavoriteProject favoriteProject, CancellationToken cancellationToken = default);
        Task<bool> RemoveFavoriteAsync(int userId, int projectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetFavoriteProjectsByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        // Method for updating rating
        Task<bool> UpdateRatingAsync(int projectId, double rating, CancellationToken cancellationToken = default);
    }
}
