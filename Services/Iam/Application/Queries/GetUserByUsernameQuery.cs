using Application.DTOs;
using MediatR;

namespace Application.Queries;

public record GetUserByUsernameQuery(string Username) : IRequest<UserDto?>;
