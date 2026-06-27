using LibraryManagement.Application.DTOs.Publisher;
using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommand : IRequest<PublisherDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}