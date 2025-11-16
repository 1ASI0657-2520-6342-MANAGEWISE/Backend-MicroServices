using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Application.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return null;
            }
            // TODO: Map User entity to UserDto, including Company details
            return new UserDto(
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
            );
        }
    }
}
