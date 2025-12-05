using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For IEventRepository
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            Name = request.Name,
            Date = request.Date,
            Location = request.Location,
            Description = request.Description,
            Color = request.Color,
            ProjectId = request.ProjectId
        };

        var createdEvent = await _eventRepository.AddAsync(newEvent);

        return new EventDto(
            createdEvent.Id,
            createdEvent.Name,
            createdEvent.Date,
            createdEvent.Location,
            createdEvent.Description,
            createdEvent.Color,
            createdEvent.ProjectId
        );
    }
}
