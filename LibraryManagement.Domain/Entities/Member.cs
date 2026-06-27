namespace LibraryManagement.Domain.Entities;

public class Member : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime MembershipStartDate { get; set; } = DateTime.UtcNow;
    public DateTime? MembershipEndDate { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
}