using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record ValidateRegisterCodeCommand(string TeamRegisterCode) : IRequest<GetCompanyResource>;
}
