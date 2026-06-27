using LibraryManagement.Application.DTOs.Publisher;
using LibraryManagement.Application.Features.Publishers.Commands.CreatePublisher;
using LibraryManagement.Application.Features.Publishers.Commands.UpdatePublisher;
using LibraryManagement.Application.Features.Publishers.Commands.DeletePublisher;
using LibraryManagement.Application.Features.Publishers.Queries.GetAllPublishers;
using LibraryManagement.Application.Features.Publishers.Queries.GetPublisherById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PublishersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPublishersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPublisherByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Create([FromBody] CreatePublisherDto dto)
    {
        var result = await _mediator.Send(new CreatePublisherCommand
        {
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePublisherDto dto)
    {
        var result = await _mediator.Send(new UpdatePublisherCommand
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePublisherCommand { Id = id });
        return NoContent();
    }
}