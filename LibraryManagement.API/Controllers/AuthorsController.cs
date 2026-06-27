using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;
using LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;
using LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;
using LibraryManagement.Application.Features.Authors.Queries.GetAllAuthors;
using LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllAuthorsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
    {
        var result = await _mediator.Send(new CreateAuthorCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Bio = dto.Bio
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto dto)
    {
        var result = await _mediator.Send(new UpdateAuthorCommand
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Bio = dto.Bio
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteAuthorCommand { Id = id });
        return NoContent();
    }
}