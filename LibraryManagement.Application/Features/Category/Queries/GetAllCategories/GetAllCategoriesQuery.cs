using LibraryManagement.Application.DTOs.Category;
using MediatR;

namespace LibraryManagement.Application.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}