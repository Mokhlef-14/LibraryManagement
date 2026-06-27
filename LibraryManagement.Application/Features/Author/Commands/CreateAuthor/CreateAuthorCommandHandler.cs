using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Application.Data;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
{
    private readonly IAppDbContext _context;

    public CreateAuthorCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Bio = request.Bio
        };

        _context.Authors.Add(author);
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