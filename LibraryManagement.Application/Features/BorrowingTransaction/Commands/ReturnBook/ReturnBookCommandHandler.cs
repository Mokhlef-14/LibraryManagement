using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.BorrowingTransaction;
using LibraryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Commands.ReturnBook;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, BorrowingTransactionDto>
{
    private readonly IAppDbContext _context;

    public ReturnBookCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BorrowingTransactionDto> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.BorrowingTransactions
            .Include(t => t.Book)
            .Include(t => t.Member)
            .Include(t => t.SystemUser)
            .FirstOrDefaultAsync(t => t.Id == request.TransactionId, cancellationToken);

        if (transaction == null)
            throw new KeyNotFoundException($"Transaction with ID {request.TransactionId} not found");

        if (transaction.Status == TransactionStatus.Returned)
            throw new InvalidOperationException("Book already returned");

        transaction.ReturnDate = DateTime.UtcNow;
        transaction.Status = transaction.DueDate < DateTime.UtcNow
            ? TransactionStatus.Overdue
            : TransactionStatus.Returned;

        if (request.Notes != null)
            transaction.Notes = request.Notes;

        transaction.Book.Status = BookStatus.Available;

        await _context.SaveChangesAsync(cancellationToken);

        return new BorrowingTransactionDto
        {
            Id = transaction.Id,
            BookTitle = transaction.Book.Title,
            MemberName = $"{transaction.Member.FirstName} {transaction.Member.LastName}",
            ProcessedBy = $"{transaction.SystemUser.FirstName} {transaction.SystemUser.LastName}",
            BorrowDate = transaction.BorrowDate,
            DueDate = transaction.DueDate,
            ReturnDate = transaction.ReturnDate,
            Status = transaction.Status.ToString(),
            Notes = transaction.Notes
        };
    }
}