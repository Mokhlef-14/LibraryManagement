using LibraryManagement.Application.DTOs.Publisher;
using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Queries.GetAllPublishers;

public class GetAllPublishersQuery : IRequest<IEnumerable<PublisherDto>>
{
}