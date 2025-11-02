using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetTasksByCompanyIdQuery(int CompanyId) : IRequest<IEnumerable<TaskItemDto>>;
}
