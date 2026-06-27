using LibraryManagement.Application.DTOs.Publisher;
using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommand : IRequest<PublisherDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}