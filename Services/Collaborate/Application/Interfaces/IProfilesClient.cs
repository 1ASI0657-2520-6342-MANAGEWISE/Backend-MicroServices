using System.Threading;
using System.Threading.Tasks;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IProfilesClient
{
    Task<ProfilesUserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default);
}