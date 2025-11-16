using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // TODO: Consider adding a dedicated endpoint for UpdateProjectStatusCommand if needed.
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] int companyId)
        {
            var query = new GetAllProjectsQuery(companyId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var query = new GetProjectByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch("{projectId}/rating")]
        public async Task<IActionResult> UpdateRating(int projectId, [FromBody] UpdateRatingCommand command)
        {
            if (projectId != command.ProjectId)
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{projectId}/team")]
        public async Task<IActionResult> GetTeamMembers(int projectId)
        {
            var query = new GetAllTeamMembersByProjectIdQuery(projectId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var command = new DeleteProjectCommand(projectId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUser(int userId)
        {
            var query = new GetAllProjectsByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> SaveProjectAsFavorite([FromBody] SaveProjectAsFavoriteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("favorite")]
        public async Task<IActionResult> DeleteProjectFromFavorite([FromBody] DeleteProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("favorite/user/{userId}")]
        public async Task<IActionResult> GetFavoriteProjectsByUser(int userId)
        {
            var query = new GetFavoriteProjectsByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
