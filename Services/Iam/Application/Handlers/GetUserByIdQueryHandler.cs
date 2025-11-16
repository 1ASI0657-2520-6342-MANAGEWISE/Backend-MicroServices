using Application.Contracts;
using Application.DTOs;
using Application.Queries;
using MediatR;

namespace Application.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByIdAsync(request.Id);
        return user == null
            ? null
            : new UserDto(user.Id, user.Username, (int)user.Role);
    }
}
