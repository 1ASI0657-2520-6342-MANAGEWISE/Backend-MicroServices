using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.QueryHandlers;

public class GetEventsByProjectIdQueryHandler : IRequestHandler<GetEventsByProjectIdQuery, IEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsByProjectIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDto>> Handle(GetEventsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetByProjectIdAsync(request.ProjectId);
        return events.Select(e => new EventDto(
            e.Id,
            e.Name,
            e.Date,
            e.Location,
            e.Description,
            e.Color,
            e.ProjectId
        )).ToList();
    }
}
