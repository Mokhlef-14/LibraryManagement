using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.Member;
using LibraryManagement.Domain.Entities;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
{
    private readonly IAppDbContext _context;

    public CreateMemberCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            MembershipEndDate = request.MembershipEndDate
        };

        _context.Members.Add(member);
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