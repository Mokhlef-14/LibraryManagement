using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int Id { get; set; }
}