using LibraryManagement.Application.DTOs.Book;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.SearchBooks;

public class SearchBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Category { get; set; }
}