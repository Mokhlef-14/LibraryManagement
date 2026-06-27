namespace LibraryManagement.Domain.Entities;

public class Publisher : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    // Navigation
    public ICollection<Book> Books { get; set; } = new List<Book>();
}