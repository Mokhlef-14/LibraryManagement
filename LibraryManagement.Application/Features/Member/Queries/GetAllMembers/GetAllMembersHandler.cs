using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.Member;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Members.Queries.GetAllMembers;

public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberDto>>
{
    private readonly IAppDbContext _context;

    public GetAllMembersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Members
            .Select(m => new MemberDto
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                Phone = m.Phone,
                Address = m.Address,
                MembershipStartDate = m.MembershipStartDate,
                MembershipEndDate = m.MembershipEndDate,
                IsActive = m.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}