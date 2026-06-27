using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Members.Commands.DeleteMember;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteMemberCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (member == null)
            throw new KeyNotFoundException($"Member with ID {request.Id} not found");

        _context.Members.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}