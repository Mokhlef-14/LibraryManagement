using LibraryManagement.Application.DTOs.SystemUser;
using LibraryManagement.Application.Features.SystemUsers.Commands.CreateSystemUser;
using LibraryManagement.Application.Features.SystemUsers.Commands.UpdateSystemUser;
using LibraryManagement.Application.Features.SystemUsers.Commands.DeleteSystemUser;
using LibraryManagement.Application.Features.SystemUsers.Queries.GetAllSystemUsers;
using LibraryManagement.Application.Features.SystemUsers.Queries.GetSystemUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class SystemUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SystemUsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSystemUsersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetSystemUserByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSystemUserDto dto)
    {
        var result = await _mediator.Send(new CreateSystemUserCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSystemUserDto dto)
    {
        var result = await _mediator.Send(new UpdateSystemUserCommand
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Role = dto.Role,
            IsActive = dto.IsActive
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteSystemUserCommand { Id = id });
        return NoContent();
    }
}