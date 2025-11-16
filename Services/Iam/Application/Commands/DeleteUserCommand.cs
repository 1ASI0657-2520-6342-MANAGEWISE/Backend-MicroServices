using MediatR;

namespace Application.Commands;

public record DeleteUserCommand(string Username) : IRequest<bool>;
