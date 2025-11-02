using MediatR;

namespace Application.Commands
{
    public record DeleteTaskItemCommand(int Id) : IRequest<bool>;
}
