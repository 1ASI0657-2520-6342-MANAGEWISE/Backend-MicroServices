using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record CreateCompanyCommand(string CompanyName, string Country, string Email, int UserId) : IRequest<GetCompanyResource>;
}
