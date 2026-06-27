using LibraryManagement.Application.DTOs.Publisher;
using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Queries.GetPublisherById;

public class GetPublisherByIdQuery : IRequest<PublisherDto>
{
    public int Id { get; set; }
}