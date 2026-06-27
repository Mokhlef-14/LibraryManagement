using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.Member;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById;

public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
{
    private readonly IAppDbContext _context;

    public GetMemberByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (member == null)
            throw new KeyNotFoundException($"Member with ID {request.Id} not found");

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