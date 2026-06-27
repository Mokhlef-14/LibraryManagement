namespace LibraryManagement.Application.DTOs.BorrowingTransaction;

public class CreateBorrowingTransactionDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime DueDate { get; set; }
    public string? Notes { get; set; }
}