using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.SystemUser;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserCommandHandler : IRequestHandler<CreateSystemUserCommand, SystemUserDto>
{
    private readonly IAppDbContext _context;

    public CreateSystemUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemUserDto> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<object>();

        var user = new SystemUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = hasher.HashPassword(null!, request.Password),
            Role = Enum.Parse<UserRole>(request.Role)
        };

        _context.SystemUsers.Add(user);
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