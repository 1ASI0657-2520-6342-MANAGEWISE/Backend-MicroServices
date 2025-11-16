using MediatR;

namespace AidManager.API.Services.Profiles.Application.Commands
{
    public record PatchImageCommand(int UserId, string Image) : IRequest<bool>;
}
