using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IAppDbContext _context;

    public GetAuthorByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (author == null)
            throw new KeyNotFoundException($"Author with ID {request.Id} not found");

        return new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio
        };
    }
}