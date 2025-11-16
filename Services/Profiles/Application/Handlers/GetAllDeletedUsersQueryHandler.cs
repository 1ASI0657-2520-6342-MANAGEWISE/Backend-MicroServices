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
    public class GetAllDeletedUsersQueryHandler : IRequestHandler<GetAllDeletedUsersQuery, IEnumerable<DeletedUserResource>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDeletedUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeletedUserResource>> Handle(GetAllDeletedUsersQuery request, CancellationToken cancellationToken)
        {
            var deletedUsers = await _unitOfWork.DeletedUsers.GetAllAsync();
            // TODO: Map DeletedUser entities to DeletedUserResource DTOs, including Company details
            return deletedUsers.Select(user => new DeletedUserResource(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}",
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                "********", // Mask password
                user.ProfileImg ?? string.Empty,
                user.Role ?? string.Empty,
                user.CompanyId,
                "CompanyName", // Placeholder
                "CompanyEmail", // Placeholder
                "CompanyCountry", // Placeholder
                user.DeletedAt
            ));
        }
    }
}
