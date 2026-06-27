using LibraryManagement.Application.DTOs.Author;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAllAuthors;

public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
}