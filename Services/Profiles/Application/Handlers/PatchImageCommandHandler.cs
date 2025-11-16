using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class PatchImageCommandHandler : IRequestHandler<PatchImageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatchImageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(PatchImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return false; // Or throw an exception
            }

            user.ProfileImg = request.Image;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
