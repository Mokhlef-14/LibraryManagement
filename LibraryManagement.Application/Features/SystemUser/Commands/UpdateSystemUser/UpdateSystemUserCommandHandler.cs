using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.SystemUser;
using LibraryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.UpdateSystemUser;

public class UpdateSystemUserCommandHandler : IRequestHandler<UpdateSystemUserCommand, SystemUserDto>
{
    private readonly IAppDbContext _context;

    public UpdateSystemUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemUserDto> Handle(UpdateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.SystemUsers
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.Role = Enum.Parse<UserRole>(request.Role);
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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