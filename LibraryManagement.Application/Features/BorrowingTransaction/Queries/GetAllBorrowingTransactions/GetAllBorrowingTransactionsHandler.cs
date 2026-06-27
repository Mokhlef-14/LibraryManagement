using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.BorrowingTransaction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetAllBorrowingTransactions;

public class GetAllBorrowingTransactionsHandler : IRequestHandler<GetAllBorrowingTransactionsQuery, IEnumerable<BorrowingTransactionDto>>
{
    private readonly IAppDbContext _context;

    public GetAllBorrowingTransactionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BorrowingTransactionDto>> Handle(GetAllBorrowingTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.BorrowingTransactions
            .Include(t => t.Book)
            .Include(t => t.Member)
            .Include(t => t.SystemUser)
            .Select(t => new BorrowingTransactionDto
            {
                Id = t.Id,
                BookTitle = t.Book.Title,
                MemberName = $"{t.Member.FirstName} {t.Member.LastName}",
                ProcessedBy = $"{t.SystemUser.FirstName} {t.SystemUser.LastName}",
                BorrowDate = t.BorrowDate,
                DueDate = t.DueDate,
                ReturnDate = t.ReturnDate,
                Status = t.Status.ToString(),
                Notes = t.Notes
            })
            .ToListAsync(cancellationToken);
    }
}