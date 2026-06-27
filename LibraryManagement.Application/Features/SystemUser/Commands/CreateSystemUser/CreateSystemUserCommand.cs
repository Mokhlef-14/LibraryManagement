using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserCommand : IRequest<SystemUserDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}