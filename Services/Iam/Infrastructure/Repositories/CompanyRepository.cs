using Application.Contracts;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IamDbContext _context;

        public CompanyRepository(IamDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> GetByEmailAsync(string email)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Company> GetByTeamRegisterCodeAsync(string teamRegisterCode)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.TeamRegisterCode == teamRegisterCode);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
