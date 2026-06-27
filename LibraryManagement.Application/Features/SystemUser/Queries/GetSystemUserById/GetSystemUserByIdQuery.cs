using LibraryManagement.Application.DTOs.SystemUser;
using MediatR;

namespace LibraryManagement.Application.Features.SystemUsers.Queries.GetSystemUserById;

public class GetSystemUserByIdQuery : IRequest<SystemUserDto>
{
    public int Id { get; set; }
}