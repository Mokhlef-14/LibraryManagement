using LibraryManagement.Application.DTOs.BorrowingTransaction;
using MediatR;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetBorrowingTransactionById;

public class GetBorrowingTransactionByIdQuery : IRequest<BorrowingTransactionDto>
{
    public int Id { get; set; }
}