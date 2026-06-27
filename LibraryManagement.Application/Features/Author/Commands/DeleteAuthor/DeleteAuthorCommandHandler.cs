using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteAuthorCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (author == null)
            throw new KeyNotFoundException($"Author with ID {request.Id} not found");

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}