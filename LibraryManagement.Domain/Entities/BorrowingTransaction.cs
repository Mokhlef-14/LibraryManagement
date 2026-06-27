using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities;

public class BorrowingTransaction : BaseEntity
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int SystemUserId { get; set; }
    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Active;
    public string? Notes { get; set; }

    // Navigation
    public Book Book { get; set; } = null!;
    public Member Member { get; set; } = null!;
    public SystemUser SystemUser { get; set; } = null!;
}