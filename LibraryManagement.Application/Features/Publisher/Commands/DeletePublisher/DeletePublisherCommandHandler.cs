using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeletePublisherCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new KeyNotFoundException($"Publisher with ID {request.Id} not found");

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}