using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetCompanyByEmailQuery(string Email) : IRequest<GetCompanyResource>;
}
