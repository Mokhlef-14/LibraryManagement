using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
{
    private readonly IAppDbContext _context;

    public UpdateAuthorCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (author == null)
            throw new KeyNotFoundException($"Author with ID {request.Id} not found");

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.Bio = request.Bio;
        author.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio
        };
    }
}