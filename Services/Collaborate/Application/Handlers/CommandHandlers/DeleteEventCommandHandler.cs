using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces; // For IEventRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
        if (eventItem == null)
        {
            return false;
        }
        await _eventRepository.DeleteAsync(request.EventId);
        return true;
    }
}
