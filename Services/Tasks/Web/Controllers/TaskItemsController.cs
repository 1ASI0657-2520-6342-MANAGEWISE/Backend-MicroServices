using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// Corrected namespace

// Corrected namespace
// Specific using for each command/query might be needed if names clash, or keep as is if specific enough.
// For now, assuming direct command/query names are unique enough or will be qualified inline if needed.
// The previous version was very specific, which is safer. Let's adjust to use the correct base and then the specific command/query type.


namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var query = new GetTaskItemByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] UpdateTaskItemCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var command = new DeleteTaskItemCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems([FromQuery] int projectId)
        {
            var query = new GetTasksByProjectIdQuery(projectId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetAllTaskItemsByCompany(int companyId)
        {
            var query = new GetTasksByCompanyIdQuery(companyId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("user/{userId}/company/{companyId}")]
        public async Task<IActionResult> GetAllTaskItemsByUserByCompany(int userId, int companyId)
        {
            var query = new GetAllTasksByUserIdByCompanyIdQuery(userId, companyId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllTaskItemsByUser(int userId)
        {
            var query = new GetTasksByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
