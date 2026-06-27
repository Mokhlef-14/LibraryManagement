using LibraryManagement.Application.DTOs.BorrowingTransaction;
using MediatR;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Commands.CreateBorrowingTransaction;

public class CreateBorrowingTransactionCommand : IRequest<BorrowingTransactionDto>
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int SystemUserId { get; set; }
    public DateTime DueDate { get; set; }
    public string? Notes { get; set; }
}