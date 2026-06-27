using LibraryManagement.Application.Data;
using LibraryManagement.Application.DTOs.Auth;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAppDbContext _context;
    private readonly JwtService _jwtService;

    public LoginCommandHandler(IAppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.SystemUsers
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null!, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid email or password");

        user.LastLoginAt = DateTime.UtcNow;

        _context.UserActivityLogs.Add(new UserActivityLog
        {
            SystemUserId = user.Id,
            Action = "Login",
            Details = $"User {user.Email} logged in",
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Role = user.Role.ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        };
    }
}