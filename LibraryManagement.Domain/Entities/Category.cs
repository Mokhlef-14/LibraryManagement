namespace LibraryManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Self-referencing (Hierarchical)
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    // Navigation
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}