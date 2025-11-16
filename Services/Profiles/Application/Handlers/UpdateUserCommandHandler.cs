using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return null; // Or throw an exception
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Age = request.Age;
            user.Phone = request.Phone;
            user.ProfileImg = request.ProfileImg;
            user.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = request.Password; // Remember to hash the new password
            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

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
                "CompanyName", // Placeholder - fetch actual company name
                "CompanyEmail", // Placeholder - fetch actual company email
                "CompanyCountry" // Placeholder - fetch actual company country
            );
        }
    }
}
