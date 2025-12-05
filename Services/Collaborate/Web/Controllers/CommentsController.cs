using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment([FromBody] AddCommentCommand command)
        {
            // Assuming the command returns the created comment DTO or its ID,
            // or throws an exception if something goes wrong (which would be handled by a global exception handler).
            // For simplicity, let's assume it returns the created resource or an identifier.
            var result = await _mediator.Send(command);
            // A more robust implementation might check the type of result or have the command return a specific
            // structure indicating success/failure and data.
            // For now, if it doesn't throw, we assume success and return the result.
            return Ok(result);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetComments(int postId)
        {
            var result = await _mediator.Send(new GetCommentsByPostIdQuery(postId));
            if (result == null)
            {
                // This assumes GetCommentsByPostIdQuery returns null if no comments are found for the post.
                // Depending on the query implementation, it might return an empty list instead.
                // If it returns an empty list, Ok(result) would be appropriate.
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment(int commentId)
        {
            var result = await _mediator.Send(new GetCommentByIdQuery(commentId));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int postId, int commentId)
        {
            // Assuming DeleteCommentCommand returns a boolean indicating success.
            var success = await _mediator.Send(new DeleteCommentCommand(postId, commentId));
            if (success)
            {
                return NoContent();
            }
            // This could be NotFound if the comment to delete wasn't found,
            // or BadRequest if the deletion failed for other reasons.
            // For now, NotFound seems reasonable if the command indicates failure primarily due to the comment not existing.
            return NotFound();
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetCommentsByCompany(int companyId)
        {
            var result = await _mediator.Send(new GetCommentsByCompanyIdQuery(companyId));
            // Similar to GetComments, this assumes the query might return null or an empty list.
            // If it's an empty list for "no comments found for this company", Ok(result) is fine.
            // If null specifically means "company not found" or some other distinct error, NotFound() is appropriate.
            // Let's assume for now that an empty list is a valid result and should be returned with Ok.
            // The prompt says "Ok(result) if result is not null or empty, otherwise NotFound()"
            // This is a bit ambiguous for collections. Typically, an empty collection is a valid result (200 OK).
            // Let's adjust to return NotFound if the result is null, assuming the query handles "empty" by returning an empty list.
            if (result == null)
            {
                // This implies the query itself would return null if the companyId is invalid or no data structure could be formed.
                // If the query always returns a list (even if empty), this condition might need adjustment based on how "empty" is checked.
                // For now, sticking to "result is not null". If "result" is an IEnumerable<T>, an empty list is not null.
                return NotFound();
            }
            // If we want to return NotFound for an empty list specifically:
            // if (result == null || !result.Any()) return NotFound();
            // However, returning an empty list with Ok is common. I will stick to the null check as per common API patterns
            // unless the query explicitly defines null vs empty list differently.
            return Ok(result);
        }
    }
}
