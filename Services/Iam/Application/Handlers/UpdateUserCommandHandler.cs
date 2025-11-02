using Application.Commands;
using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.OldUsername);
        if (user == null)
            return false;

        user.Username = request.Username;
        user.PasswordHash = request.Password;
        user.Role = (UserRole)request.UserRole;
        return await _repository.Update(user);
    }
}
