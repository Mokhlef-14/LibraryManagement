using LibraryManagement.Application.DTOs.Publisher;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Publishers.Queries.GetAllPublishers;

public class GetAllPublishersHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherDto>>
{
    private readonly IAppDbContext _context;

    public GetAllPublishersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PublisherDto>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Publishers
            .Select(p => new PublisherDto
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone = p.Phone,
                Email = p.Email
            })
            .ToListAsync(cancellationToken);
    }
}