using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] CreateUserCommand command)
        {
            var userDto = await _mediator.Send(command);
            // Assuming UserDto contains an Id, or the command returns the created user's Id.
            // Adjust to return CreatedAtAction if possible, requires GetUserById to be implemented and working.
            return Ok(userDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")] // Assuming id from route is the UserId
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.UserId) // Basic validation
            {
                return BadRequest("User ID mismatch.");
            }
            var userDto = await _mediator.Send(command);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        [HttpPatch("{id}/image")] // Assuming id from route is the UserId
        public async Task<IActionResult> UpdateUserImage(int id, [FromBody] PatchImageCommand command)
        {
             if (id != command.UserId) // Basic validation
            {
                return BadRequest("User ID mismatch.");
            }
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(); // Or BadRequest if the image update itself failed for other reasons
            }
            return NoContent();
        }

        [HttpDelete("kick/{userId}")]
        public async Task<IActionResult> KickUserByCompanyId(int userId)
        {
            var command = new KickUserByCompanyIdCommand(userId);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(); // User not found or already processed
            }
            return NoContent();
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<DeletedUserResource>>> GetAllDeletedUsers()
        {
            var query = new GetAllDeletedUsersQuery();
            var deletedUsers = await _mediator.Send(query);
            return Ok(deletedUsers);
        }
    }
}
