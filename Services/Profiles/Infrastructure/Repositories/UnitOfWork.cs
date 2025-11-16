using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProfilesDbContext _context;
        public IUserRepository Users { get; }
        public IDeletedUserRepository DeletedUsers { get; }

        public UnitOfWork(ProfilesDbContext context, IUserRepository userRepository, IDeletedUserRepository deletedUserRepository)
        {
            _context = context;
            Users = userRepository;
            DeletedUsers = deletedUserRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
