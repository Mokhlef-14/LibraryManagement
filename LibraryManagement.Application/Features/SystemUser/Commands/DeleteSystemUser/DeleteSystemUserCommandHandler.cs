using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.DeleteSystemUser;

public class DeleteSystemUserCommandHandler : IRequestHandler<DeleteSystemUserCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteSystemUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteSystemUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.SystemUsers
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        _context.SystemUsers.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}