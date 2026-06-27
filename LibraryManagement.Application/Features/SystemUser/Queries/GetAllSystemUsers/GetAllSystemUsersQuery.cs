using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;

namespace LibraryManagement.Application.Features.SystemUsers.Queries.GetAllSystemUsers;

public class GetAllSystemUsersQuery : IRequest<IEnumerable<SystemUserDto>>
{
}