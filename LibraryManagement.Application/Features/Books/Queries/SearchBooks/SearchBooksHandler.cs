using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Queries.SearchBooks;

public class SearchBooksHandler : IRequestHandler<SearchBooksQuery, IEnumerable<BookDto>>
{
    private readonly IAppDbContext _context;

    public SearchBooksHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(b => b.Title.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Author))
            query = query.Where(b => b.BookAuthors.Any(ba =>
                (ba.Author.FirstName + " " + ba.Author.LastName).Contains(request.Author)));

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(b => b.BookCategories.Any(bc =>
                bc.Category.Name.Contains(request.Category)));

        return await query.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            ISBN = b.ISBN,
            Edition = b.Edition,
            Summary = b.Summary,
            CoverImageUrl = b.CoverImageUrl,
            PublicationYear = b.PublicationYear,
            Language = b.Language,
            Status = b.Status.ToString(),
            PublisherName = b.Publisher.Name,
            Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}").ToList(),
            Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList()
        }).ToListAsync(cancellationToken);
    }
}