using LibraryManagement.Application.DTOs.Publisher;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, PublisherDto>
{
    private readonly IAppDbContext _context;

    public UpdatePublisherCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PublisherDto> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new KeyNotFoundException($"Publisher with ID {request.Id} not found");

        publisher.Name = request.Name;
        publisher.Address = request.Address;
        publisher.Phone = request.Phone;
        publisher.Email = request.Email;
        publisher.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new PublisherDto
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Address = publisher.Address,
            Phone = publisher.Phone,
            Email = publisher.Email
        };
    }
}