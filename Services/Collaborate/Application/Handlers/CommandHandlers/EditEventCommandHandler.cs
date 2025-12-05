using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For IEventRepository
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class EditEventCommandHandler : IRequestHandler<EditEventCommand, EventDto?>
{
    private readonly IEventRepository _eventRepository;

    public EditEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto?> Handle(EditEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.Id);
        if (eventItem == null)
        {
            return null; // Or throw NotFoundException
        }

        eventItem.Name = request.Name;
        eventItem.Date = request.Date;
        eventItem.Location = request.Location;
        eventItem.Description = request.Description;
        // eventItem.Color = request.Color; // If color is part of EditEventCommand

        await _eventRepository.UpdateAsync(eventItem);

        return new EventDto(
            eventItem.Id,
            eventItem.Name,
            eventItem.Date,
            eventItem.Location,
            eventItem.Description,
            eventItem.Color, // Use existing color if not updated
            eventItem.ProjectId
        );
    }
}
