using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class KickUserByCompanyIdCommandHandler : IRequestHandler<KickUserByCompanyIdCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public KickUserByCompanyIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(KickUserByCompanyIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return false; // Or throw an exception
            }

            var deletedUser = new DeletedUser
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Age = user.Age,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone ?? string.Empty,
                Password = user.Password ?? string.Empty,
                ProfileImg = user.ProfileImg ?? string.Empty,
                CompanyId = user.CompanyId,
                Role = user.Role.ToString(), // Assuming Role needs conversion
                DeletedAt = DateTime.UtcNow
            };

            await _unitOfWork.DeletedUsers.AddAsync(deletedUser);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
