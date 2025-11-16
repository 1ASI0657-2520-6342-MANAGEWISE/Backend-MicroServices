using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetCompanyByIdQuery(int CompanyId) : IRequest<GetCompanyResource>;
}
