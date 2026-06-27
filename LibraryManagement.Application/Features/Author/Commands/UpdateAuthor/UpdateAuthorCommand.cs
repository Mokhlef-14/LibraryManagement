using LibraryManagement.Application.DTOs.Author;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand : IRequest<AuthorDto>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Bio { get; set; }
}