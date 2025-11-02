using MediatR;

namespace Application.Commands
{
    public record DeleteProjectCommand(int ProjectId) : IRequest<bool>;
}
