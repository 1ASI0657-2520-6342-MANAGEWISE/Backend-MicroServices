using MediatR;

namespace Application.Commands
{
    public record RemoveProjectAsFavoriteCommand(int UserId, int ProjectId) : IRequest<bool>;
}
