using AidManager.API.Services.Profiles.Application.DTOs;
using MediatR;

namespace AidManager.API.Services.Profiles.Application.Commands
{
    public record UpdateUserCommand(
        int UserId, // Added UserId to identify the user to update
        string FirstName,
        string LastName,
        int Age,
        string Phone,
        string ProfileImg,
        string Email,
        string Password) : IRequest<UserDto?>;
}
