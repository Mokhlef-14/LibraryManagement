using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.Member;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, MemberDto>
{
    private readonly IAppDbContext _context;

    public UpdateMemberCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<MemberDto> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (member == null)
            throw new KeyNotFoundException($"Member with ID {request.Id} not found");

        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Email = request.Email;
        member.Phone = request.Phone;
        member.Address = request.Address;
        member.MembershipEndDate = request.MembershipEndDate;
        member.IsActive = request.IsActive;
        member.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new MemberDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
            Address = member.Address,
            MembershipStartDate = member.MembershipStartDate,
            MembershipEndDate = member.MembershipEndDate,
            IsActive = member.IsActive
        };
    }
}