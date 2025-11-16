using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Application.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            // TODO: Map User entities to UserDtos, including Company details for each user
            return users.Select(user => new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}",
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                "********", // Mask password
                user.ProfileImg ?? string.Empty,
                user.Role.ToString(), // Assuming Role is an enum or needs conversion
                user.CompanyId,
                "CompanyName", // Placeholder
                "CompanyEmail", // Placeholder
                "CompanyCountry" // Placeholder
            ));
        }
    }
}
