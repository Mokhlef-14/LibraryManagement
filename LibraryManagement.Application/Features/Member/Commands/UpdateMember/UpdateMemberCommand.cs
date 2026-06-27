using LibraryManagement.Application.DTOs.Member;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberCommand : IRequest<MemberDto>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime? MembershipEndDate { get; set; }
    public bool IsActive { get; set; }
}