using AidManager.API.Services.Profiles.Application.DTOs;
using MediatR;

namespace AidManager.API.Services.Profiles.Application.Queries
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto?>;
}
