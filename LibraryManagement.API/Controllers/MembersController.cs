using LibraryManagement.Application.DTOs.Member;
using LibraryManagement.Application.Features.Members.Commands.CreateMember;
using LibraryManagement.Application.Features.Members.Commands.UpdateMember;
using LibraryManagement.Application.Features.Members.Commands.DeleteMember;
using LibraryManagement.Application.Features.Members.Queries.GetAllMembers;
using LibraryManagement.Application.Features.Members.Queries.GetMemberById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMembersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMemberByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Create([FromBody] CreateMemberDto dto)
    {
        var result = await _mediator.Send(new CreateMemberCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            MembershipEndDate = dto.MembershipEndDate
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto dto)
    {
        var result = await _mediator.Send(new UpdateMemberCommand
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            MembershipEndDate = dto.MembershipEndDate,
            IsActive = dto.IsActive
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteMemberCommand { Id = id });
        return NoContent();
    }
}