using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using AidManager.API.Services.Profiles.Infrastructure.Persistence; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        
        private readonly ProfilesDbContext _context;

        public CompanyRepository(ProfilesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company)
        {
            await _context.Set<Company>().AddAsync(company);
        }

        public async Task<Company?> GetByCodeAsync(string teamCode)
        {
            return await _context.Set<Company>()
                .FirstOrDefaultAsync(c => c.TeamRegisterCode == teamCode);
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Set<Company>().FindAsync(id);
        }
    }
}