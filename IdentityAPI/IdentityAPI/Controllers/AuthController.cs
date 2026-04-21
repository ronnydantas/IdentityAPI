using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Domain.DTOs;
using MediatR;
using IdentityAPI.UseCases.Auth;

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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _mediator.Send(new RegisterCommand(model));
            return Ok(new { message = "User registered successfully" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SignInDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var sso = await _mediator.Send(new LoginQuery(model));
            return Ok(sso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
