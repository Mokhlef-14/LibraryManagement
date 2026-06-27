using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.SystemUsers.Queries.GetSystemUserById;

public class GetSystemUserByIdHandler : IRequestHandler<GetSystemUserByIdQuery, SystemUserDto>
{
    private readonly IAppDbContext _context;

    public GetSystemUserByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemUserDto> Handle(GetSystemUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.SystemUsers
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        return new SystemUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            LastLoginAt = user.LastLoginAt
        };
    }
}