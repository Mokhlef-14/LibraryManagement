using LibraryManagement.Application.Data;
using LibraryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Books.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteBookCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null)
            throw new KeyNotFoundException($"Book with ID {request.Id} not found");

        _context.Books.Remove(book);

        _context.UserActivityLogs.Add(new UserActivityLog
        {
            SystemUserId = 0,
            Action = "DeleteBook",
            Details = $"Deleted book ID: {request.Id}",
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}