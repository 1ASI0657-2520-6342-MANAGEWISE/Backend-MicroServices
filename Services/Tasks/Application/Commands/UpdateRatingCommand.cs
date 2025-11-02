using MediatR;

namespace Application.Commands
{
    public record UpdateRatingCommand(int ProjectId, double Rating) : IRequest<bool>;
}
