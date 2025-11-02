using Domain.Entities;

namespace Application.Contracts
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(int id);
        Task<Company> GetByEmailAsync(string email);
        Task<Company> GetByTeamRegisterCodeAsync(string teamRegisterCode);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}
