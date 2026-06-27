using LibraryManagement.Application.DTOs.BorrowingTransaction;
using MediatR;

namespace LibraryManagement.Application.Features.BorrowingTransactions.Queries.GetAllBorrowingTransactions;

public class GetAllBorrowingTransactionsQuery : IRequest<IEnumerable<BorrowingTransactionDto>>
{
}