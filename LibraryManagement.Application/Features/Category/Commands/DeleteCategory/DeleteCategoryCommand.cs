using MediatR;

namespace LibraryManagement.Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}