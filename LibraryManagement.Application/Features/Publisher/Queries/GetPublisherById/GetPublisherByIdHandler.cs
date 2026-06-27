using LibraryManagement.Application.DTOs.Publisher;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Publishers.Queries.GetPublisherById;

public class GetPublisherByIdHandler : IRequestHandler<GetPublisherByIdQuery, PublisherDto>
{
    private readonly IAppDbContext _context;

    public GetPublisherByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PublisherDto> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new KeyNotFoundException($"Publisher with ID {request.Id} not found");

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