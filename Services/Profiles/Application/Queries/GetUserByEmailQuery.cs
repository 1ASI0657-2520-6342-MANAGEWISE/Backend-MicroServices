using AidManager.API.Services.Profiles.Application.DTOs;
using MediatR;

namespace AidManager.API.Services.Profiles.Application.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;
}