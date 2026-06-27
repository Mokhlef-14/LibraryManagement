using LibraryManagement.Application.DTOs.BorrowingTransaction;
using LibraryManagement.Application.Features.BorrowingTransactions.Commands.CreateBorrowingTransaction;
using LibraryManagement.Application.Features.BorrowingTransactions.Commands.ReturnBook;
using LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetAllBorrowingTransactions;
using LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetBorrowingTransactionById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BorrowingTransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BorrowingTransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllBorrowingTransactionsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetBorrowingTransactionByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Borrow([FromBody] CreateBorrowingTransactionDto dto)
    {
        var result = await _mediator.Send(new CreateBorrowingTransactionCommand
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            SystemUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value),
            DueDate = dto.DueDate,
            Notes = dto.Notes
        });
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}/return")]
    [Authorize(Roles = "Administrator,Librarian")]
    public async Task<IActionResult> Return(int id, [FromBody] ReturnBookDto dto)
    {
        var result = await _mediator.Send(new ReturnBookCommand
        {
            TransactionId = id,
            Notes = dto.Notes
        });
        return Ok(result);
    }
}