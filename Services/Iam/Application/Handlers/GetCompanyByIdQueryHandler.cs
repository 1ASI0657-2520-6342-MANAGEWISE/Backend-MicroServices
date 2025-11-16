using Application.Contracts;
using Application.DTOs;
using Application.Queries;
using MediatR;

namespace Application.Handlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResource> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return null; // Or throw an exception
            }

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
