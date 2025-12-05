using AidManager.API.Services.Profiles.Domain.Entities;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        
        Task<Company?> GetByCodeAsync(string teamCode);
        
        Task<Company?> GetByIdAsync(int id);
    }
}