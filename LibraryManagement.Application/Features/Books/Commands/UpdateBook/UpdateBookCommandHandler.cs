using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IAppDbContext _context;

    public UpdateBookCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.BookAuthors)
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null)
            throw new KeyNotFoundException($"Book with ID {request.Id} not found");

        book.Title = request.Title;
        book.ISBN = request.ISBN;
        book.Edition = request.Edition;
        book.Summary = request.Summary;
        book.CoverImageUrl = request.CoverImageUrl;
        book.PublicationYear = request.PublicationYear;
        book.Language = request.Language;
        book.PublisherId = request.PublisherId;
        book.UpdatedAt = DateTime.UtcNow;

        book.BookAuthors.Clear();
        foreach (var authorId in request.AuthorIds)
            book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });

        book.BookCategories.Clear();
        foreach (var categoryId in request.CategoryIds)
            book.BookCategories.Add(new BookCategory { CategoryId = categoryId });

        await _context.SaveChangesAsync(cancellationToken);

        var updated = await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.Id == book.Id, cancellationToken);

        return new BookDto
        {
            Id = updated!.Id,
            Title = updated.Title,
            ISBN = updated.ISBN,
            Edition = updated.Edition,
            Summary = updated.Summary,
            CoverImageUrl = updated.CoverImageUrl,
            PublicationYear = updated.PublicationYear,
            Language = updated.Language,
            Status = updated.Status.ToString(),
            PublisherName = updated.Publisher.Name,
            Authors = updated.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}").ToList(),
            Categories = updated.BookCategories.Select(bc => bc.Category.Name).ToList()
        };
    }
}