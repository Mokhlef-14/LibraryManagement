using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Application.Features.Books.Queries.GetBooksByStatus;
using LibraryManagement.Application.Features.Books.Queries.SearchBooks;
using LibraryManagement.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllBooksQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetBookByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? author, [FromQuery] string? category)
    {
        var result = await _mediator.Send(new SearchBooksQuery
        {
            Name = name,
            Author = author,
            Category = category
        });
        return Ok(result);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(BookStatus status)
    {
        var result = await _mediator.Send(new GetBooksByStatusQuery { Status = status });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        var result = await _mediator.Send(new CreateBookCommand
        {
            Title = dto.Title,
            ISBN = dto.ISBN,
            Edition = dto.Edition,
            Summary = dto.Summary,
            CoverImageUrl = dto.CoverImageUrl,
            PublicationYear = dto.PublicationYear,
            Language = dto.Language,
            PublisherId = dto.PublisherId,
            AuthorIds = dto.AuthorIds,
            CategoryIds = dto.CategoryIds
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        var result = await _mediator.Send(new UpdateBookCommand
        {
            Id = id,
            Title = dto.Title,
            ISBN = dto.ISBN,
            Edition = dto.Edition,
            Summary = dto.Summary,
            CoverImageUrl = dto.CoverImageUrl,
            PublicationYear = dto.PublicationYear,
            Language = dto.Language,
            PublisherId = dto.PublisherId,
            AuthorIds = dto.AuthorIds,
            CategoryIds = dto.CategoryIds
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteBookCommand { Id = id });
        return NoContent();
    }
}