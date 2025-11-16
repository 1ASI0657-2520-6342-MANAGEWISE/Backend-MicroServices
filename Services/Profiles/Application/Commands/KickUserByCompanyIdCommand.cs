using MediatR;

namespace AidManager.API.Services.Profiles.Application.Commands
{
    public record KickUserByCompanyIdCommand(int UserId) : IRequest<bool>;
}
