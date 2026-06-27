using LibraryManagement.Application.DTOs.Author;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQuery : IRequest<AuthorDto>
{
    public int Id { get; set; }
}