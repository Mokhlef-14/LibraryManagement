using LibraryManagement.Application.DTOs.BorrowingTransaction;
using MediatR;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Commands.ReturnBook;

public class ReturnBookCommand : IRequest<BorrowingTransactionDto>
{
    public int TransactionId { get; set; }
    public string? Notes { get; set; }
}