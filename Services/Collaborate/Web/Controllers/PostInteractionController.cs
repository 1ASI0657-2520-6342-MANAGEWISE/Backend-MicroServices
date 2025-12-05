using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs; // Assuming DTOs might be returned by queries or commands
using System.Threading.Tasks;

namespace AidManager.Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostInteractionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostInteractionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> FavoritePost([FromBody] FavoritePostCommand command)
        {
            // Assuming the command execution (e.g., adding a favorite)
            // returns some result, like a boolean indicating success or a status object.
            // Or it might throw an exception for errors (e.g., post not found, user not found),
            // which would be handled by a global exception handler.
            var result = await _mediator.Send(command);

            // If 'result' is, for example, a boolean true for success:
            // if (result is bool success && success) return Ok(); // Or Ok(true)
            // If it's more complex, Ok(result) is fine.
            // If the command is void (Task) and success is implied by not throwing: return Ok();
            return Ok(result);
        }

        [HttpPost("unfavorite")]
        public async Task<IActionResult> RemoveFavoritePost([FromBody] RemoveFavoritePostCommand command)
        {
            // Similar to FavoritePost, assumes the command returns a confirmation
            // or throws an exception on failure (e.g., favorite not found).
            var result = await _mediator.Send(command);

            // If 'result' is a boolean indicating success (e.g., true if unfavorited, false if not found):
            // if (result is bool success && success) return NoContent(); // Common for delete/remove operations
            // else if (result is bool s && !s) return NotFound();
            // Sticking to Ok(result) as per instructions.
            return Ok(result);
        }

        [HttpGet("user/{userId}/favorites")]
        public async Task<IActionResult> GetFavoritePosts(int userId)
        {
            var result = await _mediator.Send(new GetFavoritePostsByUserIdQuery(userId));

            // Assuming GetFavoritePostsByUserIdQuery returns a list of favorite post DTOs.
            // If 'result' is null, it implies the user was not found or has no favorites
            // (depending on query implementation, an empty list is often preferred for "no favorites").
            if (result == null)
            {
                // If an empty list is a valid response for "no favorites", then Ok(result) should be returned.
                // NotFound() here implies that either the user doesn't exist, or the query itself
                // has a specific reason to return null for "no favorites found".
                return NotFound();
            }
            return Ok(result);
        }
    }
}
