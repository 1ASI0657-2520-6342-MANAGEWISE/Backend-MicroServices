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

            return new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                "********", 
                user.ProfileImg ?? string.Empty,
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