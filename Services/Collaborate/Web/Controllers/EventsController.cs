using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Threading.Tasks;
using System.Linq; // Required for .Any() if used

namespace AidManager.Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEvent([FromBody] CreateEventCommand command)
        {
            // Assuming the command returns the created event DTO or its ID.
            // Or throws an exception handled globally.
            var result = await _mediator.Send(command);
            // For a POST request that creates a resource, CreatedAtAction is often preferred.
            // Example: return CreatedAtAction(nameof(GetEventById), new { eventId = result.Id }, result);
            // But Ok(result) is acceptable as per requirements.
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());
            // Assuming GetAllEventsQuery returns a list of event DTOs.
            // If null means "no events found" (which is unusual for a "get all" - usually an empty list),
            // then NotFound() is okay. Typically, an empty list is returned with Ok().
            if (result == null)
            {
                return NotFound(); // Or Ok(new List<EventDto>()); if an empty list is preferred over NotFound for "no events".
            }
            return Ok(result);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetEventsByProjectId(int projectId)
        {
            var result = await _mediator.Send(new GetEventsByProjectIdQuery(projectId));
            // The requirement is "Ok(result) if result is not null or empty, otherwise NotFound()."
            // This implies result is a collection.
            if (result == null) // Covers the null case
            {
                return NotFound();
            }
            // If result is an IEnumerable<T>, we need to check for emptiness.
            // This requires knowing the actual return type or using a cast/check.
            // Assuming 'result' is IEnumerable<some DTO>.
            if (result is System.Collections.IEnumerable enumerableResult && !enumerableResult.GetEnumerator().MoveNext()) // Checks for empty
            {
                 return NotFound();
            }
            return Ok(result);
        }

        // Assuming EditEventCommand is defined as:
        // public class EditEventCommand : IRequest<EventDto> // or IRequest<bool>
        // {
        //     public int Id { get; set; }
        //     public string Name { get; set; }
        //     public string Location { get; set; }
        //     public string Description { get; set; }
        //     // Constructor might be EditEventCommand(int id, string name, ...) or properties set directly
        // }
        // The subtask specified: EditEventCommand(int Id, string Name, string Location, string Description)
        // This looks like a constructor signature, implying properties Id, Name, Location, Description exist.

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EditEventCommand command)
        {
            // To make this work, EditEventCommand must have an Id property that can be compared.
            // If EditEventCommand's constructor is EditEventCommand(int Id, ...),
            // we assume the Id property is set by MediatR or the model binder based on the JSON body.
            // It's more robust to explicitly set command.Id = id if the command is designed to take it that way,
            // or ensure the command has an Id property that is part of the [FromBody] deserialization.

            // Assuming command has an 'Id' property:
            // if (id != command.Id) // This check assumes command.Id is populated from the body.
            // {
            //     return BadRequest("ID in URL must match ID in request body.");
            // }

            // A common approach for commands like EditEventCommand is that they might not have an Id property,
            // and instead take the Id as a constructor parameter when created, e.g. new EditEventCommand(id, name, ...).
            // However, the prompt shows `EditEventCommand command` (from body), which usually means all its properties are in the body.
            // Let's assume `EditEventCommand` has an `Id` property that should match the route `id`.
            // If the `command` object itself doesn't have an `Id` property to check against, this check is problematic.
            // Given `EditEventCommand(int Id, ...)` as its definition, it implies it HAS an Id.

            // Let's refine the logic slightly. If the command has an Id, it should be used.
            // If the command's Id is meant to be the authoritative one, the 'id' in the route is for routing only.
            // If the 'id' in the route is authoritative, then command.Id (if it exists) should be ignored or validated.
            // The prompt states "if (id != command.Id)" so we assume command has an Id property.

            // To make the command work as intended for MediatR, if its constructor is `EditEventCommand(int Id, ...)`
            // and it's deserialized from the body, its `Id` property must be settable and present in the JSON.
            // If we want to ensure the URL id is used, we might need to create a new command or set it.
            // For now, let's stick to the explicit check as requested.
            // This implies `EditEventCommand` should have a public `Id` property.

            // If `command.Id` is not meant to be part of the body but rather taken from the URL:
            // One way is to have a separate DTO for the body and construct the command:
            // `var updateCommand = new EditEventCommand(id, bodyDto.Name, bodyDto.Location, ...);`
            // But the prompt asks for `EditEventCommand command` from body.

            // Simplest interpretation that respects `if (id != command.Id)`:
            // EditEventCommand has an Id property.
            if (command == null || id != command.Id) // ensure command.Id can be accessed.
            {
                 return BadRequest("ID in URL must match ID in request body, or request body is invalid.");
            }

            var result = await _mediator.Send(command);
            // Assuming the command returns the updated DTO or a boolean.
            // If it returns a boolean:
            // if (result is bool success && success) return NoContent(); else return NotFound();
            // If it returns the updated DTO:
            if (result == null) // Assuming null means "not found" or "update failed"
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            // Assuming DeleteEventCommand takes eventId in constructor
            // and returns a boolean indicating success.
            var success = await _mediator.Send(new DeleteEventCommand(eventId));
            if (success)
            {
                return NoContent();
            }
            return NotFound(); // Event not found or deletion failed
        }
    }
}
