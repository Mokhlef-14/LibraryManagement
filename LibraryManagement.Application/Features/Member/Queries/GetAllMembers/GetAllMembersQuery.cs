using LibraryManagement.Application.DTOs.Member;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetAllMembers;

public class GetAllMembersQuery : IRequest<IEnumerable<MemberDto>>
{
}