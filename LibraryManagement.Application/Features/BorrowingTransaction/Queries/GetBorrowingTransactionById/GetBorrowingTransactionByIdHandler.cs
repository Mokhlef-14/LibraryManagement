using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.BorrowingTransaction;
using LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetBorrowingTransactionById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetBorrowingTransactionById;

public class GetBorrowingTransactionByIdHandler : IRequestHandler<GetBorrowingTransactionByIdQuery, BorrowingTransactionDto>
{
    private readonly IAppDbContext _context;

    public GetBorrowingTransactionByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BorrowingTransactionDto> Handle(GetBorrowingTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _context.BorrowingTransactions
            .Include(t => t.Book)
            .Include(t => t.Member)
            .Include(t => t.SystemUser)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (transaction == null)
            throw new KeyNotFoundException($"Transaction with ID {request.Id} not found");

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