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
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password, 
                ProfileImg = request.ProfileImg,
                Role = request.Role,
                
                CompanyName = request.CompanyName,
                CompanyEmail = request.CompanyEmail,
                CompanyCountry = request.CompanyCountry,
                TeamRegisterCode = request.TeamRegisterCode
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync(); 

            return new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                user.Password ?? string.Empty,
                user.ProfileImg ?? string.Empty,
                user.Role.ToString(),
                user.CompanyId,          
                user.CompanyName,        
                user.CompanyEmail,       
                user.CompanyCountry,
                user.TeamRegisterCode    
            );
        }
    }
}