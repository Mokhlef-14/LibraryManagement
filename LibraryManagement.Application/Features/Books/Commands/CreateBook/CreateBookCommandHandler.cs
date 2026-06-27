using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IAppDbContext _context;

    public CreateBookCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = request.Title,
            ISBN = request.ISBN,
            Edition = request.Edition,
            Summary = request.Summary,
            CoverImageUrl = request.CoverImageUrl,
            PublicationYear = request.PublicationYear,
            Language = request.Language,
            PublisherId = request.PublisherId
        };

        foreach (var authorId in request.AuthorIds)
            book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });

        foreach (var categoryId in request.CategoryIds)
            book.BookCategories.Add(new BookCategory { CategoryId = categoryId });

        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        return await GetBookDto(book.Id, cancellationToken);
    }

    private async Task<BookDto> GetBookDto(int bookId, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        return new BookDto
        {
            Id = book!.Id,
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