using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Domain.Enums;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBooksByStatus;

public class GetBooksByStatusQuery : IRequest<IEnumerable<BookDto>>
{
    public BookStatus Status { get; set; }
}