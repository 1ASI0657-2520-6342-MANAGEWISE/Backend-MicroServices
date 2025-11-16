using Application.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Handlers;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, User>
{
    private readonly IUserRepository _repository;

    public SignUpCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.Password,
            Role = (UserRole)request.UserRole
        };

        await _repository.AddAsync(user);
        return user;
    }
}
