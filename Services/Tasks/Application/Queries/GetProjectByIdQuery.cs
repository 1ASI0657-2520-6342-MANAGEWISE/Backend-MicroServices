using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetProjectByIdQuery(int Id) : IRequest<ProjectResource?>;
}
