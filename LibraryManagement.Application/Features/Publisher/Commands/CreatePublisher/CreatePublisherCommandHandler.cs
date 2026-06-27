using LibraryManagement.Application.DTOs.Publisher;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Application.Data;
using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, PublisherDto>
{
    private readonly IAppDbContext _context;

    public CreatePublisherCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PublisherDto> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = new Publisher
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email
        };

        _context.Publishers.Add(publisher);
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