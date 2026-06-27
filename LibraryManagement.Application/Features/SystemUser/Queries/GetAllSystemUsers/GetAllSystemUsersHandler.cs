using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.SystemUsers.Queries.GetAllSystemUsers;

public class GetAllSystemUsersHandler : IRequestHandler<GetAllSystemUsersQuery, IEnumerable<SystemUserDto>>
{
    private readonly IAppDbContext _context;

    public GetAllSystemUsersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SystemUserDto>> Handle(GetAllSystemUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.SystemUsers
            .Select(u => new SystemUserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsActive = u.IsActive,
                LastLoginAt = u.LastLoginAt
            })
            .ToListAsync(cancellationToken);
    }
}