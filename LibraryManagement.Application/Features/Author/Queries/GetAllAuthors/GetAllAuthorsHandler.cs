using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAllAuthors;

public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAppDbContext _context;

    public GetAllAuthorsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Authors
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Bio = a.Bio
            })
            .ToListAsync(cancellationToken);
    }
}