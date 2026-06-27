using MediatR;

namespace LibraryManagement.Application.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommand : IRequest<bool>
{
    public int Id { get; set; }
}