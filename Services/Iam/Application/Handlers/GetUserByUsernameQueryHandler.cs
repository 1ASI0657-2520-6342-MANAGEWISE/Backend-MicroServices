using Application.Contracts;
using Application.DTOs;
using Application.Queries;
using MediatR;

namespace Application.Handlers;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByUsernameQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.Username);
        return user == null
            ? null
            : new UserDto(user.Id, user.Username, (int)user.Role);
    }
}
