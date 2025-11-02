using Application.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers;

public class SignInCommandHandler : IRequestHandler<SignInCommand, User?>
{
    private readonly IUserRepository _repository;

    public SignInCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.Username);
        if (user == null)
            return null;

        // (In a real app, hash/check password. This is a simple example.)
        return user.PasswordHash == request.Password ? user : null;
    }
}
