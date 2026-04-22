using Identity.Domain.DTOs;
using Identity.Domain.UseCases.Auth.GetUser;
using IdentityAPI.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignUpDTO model)
    {
        await _mediator.Send(new RegisterCommand(model));

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SignInDTO model)
    {
        var sso = await _mediator.Send(new LoginQuery(model));

        return Ok(sso);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _mediator.Send(new MeQuery());
        return Ok(user);
    }
}
