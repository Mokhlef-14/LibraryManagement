using LibraryManagement.Application.DTOs.Category;
using CategoryEntity = LibraryManagement.Domain.Entities.Category;
using LibraryManagement.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity
        {
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        string? parentName = null;

        if (category.ParentCategoryId.HasValue)
        {
            var parent = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == category.ParentCategoryId, cancellationToken);

            parentName = parent?.Name;
        }

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            ParentCategoryName = parentName
        };
    }
}