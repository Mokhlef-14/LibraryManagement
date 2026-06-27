using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands;

public class DeleteBookCommand : IRequest<bool>
{
    public int Id { get; set; }
}