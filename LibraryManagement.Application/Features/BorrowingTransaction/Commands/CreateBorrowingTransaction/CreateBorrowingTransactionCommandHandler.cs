using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.BorrowingTransaction;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Commands.CreateBorrowingTransaction;

public class CreateBorrowingTransactionCommandHandler : IRequestHandler<CreateBorrowingTransactionCommand, BorrowingTransactionDto>
{
    private readonly IAppDbContext _context;

    public CreateBorrowingTransactionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BorrowingTransactionDto> Handle(CreateBorrowingTransactionCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

        if (book == null)
            throw new KeyNotFoundException($"Book with ID {request.BookId} not found");

        if (book.Status == BookStatus.Borrowed)
            throw new InvalidOperationException("Book is already borrowed");

        var transaction = new BorrowingTransaction
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            SystemUserId = request.SystemUserId,
            DueDate = request.DueDate,
            Notes = request.Notes,
            Status = TransactionStatus.Active
        };

        book.Status = BookStatus.Borrowed;

        _context.BorrowingTransactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        var result = await _context.BorrowingTransactions
            .Include(t => t.Book)
            .Include(t => t.Member)
            .Include(t => t.SystemUser)
            .FirstOrDefaultAsync(t => t.Id == transaction.Id, cancellationToken);

        return new BorrowingTransactionDto
        {
            Id = result!.Id,
            BookTitle = result.Book.Title,
            MemberName = $"{result.Member.FirstName} {result.Member.LastName}",
            ProcessedBy = $"{result.SystemUser.FirstName} {result.SystemUser.LastName}",
            BorrowDate = result.BorrowDate,
            DueDate = result.DueDate,
            ReturnDate = result.ReturnDate,
            Status = result.Status.ToString(),
            Notes = result.Notes
        };
    }
}