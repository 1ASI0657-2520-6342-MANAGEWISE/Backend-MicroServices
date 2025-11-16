using AidManager.API.Services.Profiles.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AidManager.API.Services.Profiles.Application.Queries
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
}
