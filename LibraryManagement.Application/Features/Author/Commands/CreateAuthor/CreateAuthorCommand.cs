using LibraryManagement.Application.DTOs.Author;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<AuthorDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Bio { get; set; }
}