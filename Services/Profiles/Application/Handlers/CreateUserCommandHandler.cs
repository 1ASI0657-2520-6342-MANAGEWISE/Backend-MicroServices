using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // TODO: Map CompanyName, CompanyEmail, CompanyCountry to a Company entity or handle as needed
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password, // Remember to hash the password
                ProfileImg = request.ProfileImg,
                Role = request.Role,
                // CompanyId will be set based on CompanyName, CompanyEmail, CompanyCountry
                // Or handle TeamRegisterCode if applicable for company association
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            // TODO: Map User entity to UserDto, including Company details
            return new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}",
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                user.Password ?? string.Empty, // Should not return plain password
                user.ProfileImg ?? string.Empty,
                user.Role.ToString(), // Assuming Role is an enum or needs conversion
                user.CompanyId,
                request.CompanyName,
                request.CompanyEmail,
                request.CompanyCountry
            );
        }
    }
}
