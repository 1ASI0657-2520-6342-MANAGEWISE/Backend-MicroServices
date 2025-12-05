using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Threading.Tasks;
using System.Linq; // Required for .Any()

namespace AidManager.Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPost([FromBody] CreatePostCommand command)
        {
            // Assuming command returns the created post DTO or its ID.
            var result = await _mediator.Send(command);
            // Consider CreatedAtAction for returning 201 Created:
            // return CreatedAtAction(nameof(GetPostById), new { id = result.Id }, result);
            return Ok(result);
        }

        // Assuming UpdatePostCommand has a public int Id property.
        // e.g., public class UpdatePostCommand : IRequest<PostDto> { public int Id { get; set; } ... }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostCommand command)
        {
            if (command == null || id != command.Id)
            {
                return BadRequest("ID in URL must match ID in request body, or request body is invalid.");
            }
            var result = await _mediator.Send(command);
            // Assuming command returns updated DTO. If it returns null (e.g., post not found for update),
            // NotFound() would be appropriate.
            if (result == null)
            {
                return NotFound(); // Or if command returns bool: if (!result) return NotFound();
            }
            return Ok(result);
        }

        // Assuming UpdatePostRatingCommand is defined like:
        // public class UpdatePostRatingCommand : IRequest<PostRatingDto> // or similar
        // {
        //     public int PostId { get; set; }
        //     public int UserId { get; set; }
        //     // Potentially other properties like the rating value itself, if not implicit
        // }
        // The prompt stated: UpdatePostRatingCommand(int PostId, int UserId)
        // This implies these are properties on the command object, filled from the body.
        [HttpPatch("{postId}/rating")]
        public async Task<IActionResult> UpdatePostRating(int postId, [FromBody] UpdatePostRatingCommand command)
        {
            if (command == null || postId != command.PostId)
            {
                return BadRequest("PostID in URL must match PostID in request body, or request body is invalid.");
            }
            var result = await _mediator.Send(command);
            // Assuming command returns new rating or updated post DTO.
            // If result could be null indicating "post not found" or "user not found".
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostById(int id)
        {
            // Assuming DeletePostCommand returns a boolean indicating success.
            var success = await _mediator.Send(new DeletePostCommand(id));
            if (success)
            {
                return NoContent();
            }
            return NotFound(); // Post not found or deletion failed.
        }

        private IActionResult HandleQueryResultList<T>(System.Collections.Generic.IEnumerable<T> result)
        {
            if (result == null) // Query itself failed or main entity not found
            {
                return NotFound();
            }
            if (!result.Any()) // Main entity found, but no related items
            {
                // As per "not null or empty, otherwise NotFound()"
                return NotFound();
                // Alternative: return Ok(result); // Return 200 with empty list, common practice
            }
            return Ok(result);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetAllPostsByCompanyId(int companyId)
        {
            var result = await _mediator.Send(new GetAllPostsByCompanyIdQuery(companyId));
            return HandleQueryResultList(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllPostsByUserId(int userId)
        {
            var result = await _mediator.Send(new GetAllPostsByUserIdQuery(userId));
            return HandleQueryResultList(result);
        }

        [HttpGet("user/{userId}/liked")]
        public async Task<IActionResult> GetLikedPostsByUserId(int userId)
        {
            var result = await _mediator.Send(new GetLikedPostsByUserIdQuery(userId));
            return HandleQueryResultList(result);
        }
    }
}
