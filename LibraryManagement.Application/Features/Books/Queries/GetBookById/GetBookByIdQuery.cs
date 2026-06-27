using LibraryManagement.Application.DTOs.Book;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public int Id { get; set; }
}