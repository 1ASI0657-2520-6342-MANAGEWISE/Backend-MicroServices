using MediatR;

namespace Application.Commands
{
    public record DeleteCompanyCommand(int CompanyId) : IRequest<bool>;
}
