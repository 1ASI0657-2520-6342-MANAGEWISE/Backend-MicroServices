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
                return null; 
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Age = request.Age;
            user.Phone = request.Phone;
            user.ProfileImg = request.ProfileImg;
            user.Email = request.Email;
            user.Occupation = request.Occupation;
    		user.Bio = request.Bio;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = request.Password; 
            }

          

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                "********", 
                user.ProfileImg ?? string.Empty,
				user.Occupation ?? string.Empty, 
        		user.Bio ?? string.Empty,   
                user.Role.ToString(), 
                user.CompanyId,
            
                user.CompanyName ?? string.Empty, 
                user.CompanyEmail ?? string.Empty, 
                user.CompanyCountry ?? string.Empty,
                
                user.TeamRegisterCode ?? string.Empty 
            );
        }
    }
}