using Application.Commands;
using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, GetCompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResource> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company(request.CompanyName, request.Country, request.Email, request.UserId);
            await _companyRepository.AddAsync(company);

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
