using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record EditCompanyCommand(int CompanyId, string CompanyName, string Country, string Email) : IRequest<GetCompanyResource>;
}
