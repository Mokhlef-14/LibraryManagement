using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Queries.GetBookById;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IAppDbContext _context;

    public GetBookByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null)
            throw new KeyNotFoundException($"Book with ID {request.Id} not found");

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            Edition = book.Edition,
            Summary = book.Summary,
            CoverImageUrl = book.CoverImageUrl,
            PublicationYear = book.PublicationYear,
            Language = book.Language,
            Status = book.Status.ToString(),
            PublisherName = book.Publisher.Name,
            Authors = book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}").ToList(),
            Categories = book.BookCategories.Select(bc => bc.Category.Name).ToList()
        };
    }
}