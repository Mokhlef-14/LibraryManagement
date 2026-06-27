using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Data;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Author> Authors { get; }
    DbSet<Publisher> Publishers { get; }
    DbSet<Category> Categories { get; }
    DbSet<BookAuthor> BookAuthors { get; }
    DbSet<BookCategory> BookCategories { get; }
    DbSet<Member> Members { get; }
    DbSet<SystemUser> SystemUsers { get; }
    DbSet<BorrowingTransaction> BorrowingTransactions { get; }
    DbSet<UserActivityLog> UserActivityLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}