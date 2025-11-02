using MediatR;

namespace Application.Commands
{
    // This command is unclear. Assuming 'id' refers to a new status.
    // Let's call it NewStatusId for clarity and assume it returns a boolean.
    public record UpdateProjectStatusCommand(int ProjectId, int NewStatusId) : IRequest<bool>;
}
