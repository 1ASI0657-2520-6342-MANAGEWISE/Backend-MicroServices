using System; // Add this
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable // Add IDisposable
    {
        IUserRepository Users { get; }
        IDeletedUserRepository DeletedUsers { get; }
        Task<int> CompleteAsync();
    }
}
