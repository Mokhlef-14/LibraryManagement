using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;

namespace LibraryManagement.Application.Features.SystemUsers.Commands.UpdateSystemUser;

public class UpdateSystemUserCommand : IRequest<SystemUserDto>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}