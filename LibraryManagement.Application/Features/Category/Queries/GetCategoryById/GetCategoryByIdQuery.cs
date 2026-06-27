using LibraryManagement.Application.DTOs.Category;
using MediatR;

namespace LibraryManagement.Application.Features.Category.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public int Id { get; set; }
}