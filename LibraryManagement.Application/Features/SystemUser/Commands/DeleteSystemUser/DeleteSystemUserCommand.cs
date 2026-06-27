using MediatR;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.DeleteSystemUser;

public class DeleteSystemUserCommand : IRequest<bool>
{
    public int Id { get; set; }
}