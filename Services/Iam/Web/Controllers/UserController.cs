using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInCommand command)
    {
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : Unauthorized();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto?>> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var success = await _mediator.Send(new DeleteUserCommand(username));
        return success ? NoContent() : NotFound();
    }
}
