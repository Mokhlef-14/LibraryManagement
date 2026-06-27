using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Queries.GetAllBooks;

public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IAppDbContext _context;

    public GetAllBooksHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .Select(b => new BookDto
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
            })
            .ToListAsync(cancellationToken);
    }
}