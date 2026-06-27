using LibraryManagement.Application.DTOs.Auth;
using LibraryManagement.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginCommand
        {
            Email = dto.Email,
            Password = dto.Password
        });
        return Ok(result);
    }

    [HttpPost("setup")]
    [AllowAnonymous]
    public async Task<IActionResult> Setup()
    {
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<object>();
        var hash = hasher.HashPassword(null!, "Admin@123");
        return Ok(hash);
    }
}