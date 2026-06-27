using LibraryManagement.Application.DTOs.Book;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetAllBooks;

public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
{
}