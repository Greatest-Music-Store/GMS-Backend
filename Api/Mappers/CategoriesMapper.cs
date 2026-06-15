using GMS_Backend.Domain.Models;
using GMS_Backend.DTOs.Categories;

namespace GMS_Backend.Api.Mappers;

public static class CategoriesMapper
{
    public static CategoryResponseDTO ToDto(Category category)
    {
        return new CategoryResponseDTO
        {
            Id = category.Id,
            Name = category.Name,
            Subcategories = category.Subcategories
                .Select(ToSubcategoryDto)
                .ToList()
        };
    }

    public static Category ToModel(CategoryCreationDTO dto)
    {
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };
    }

    public static SubcategoryResponseDTO ToSubcategoryDto(Subcategory subcategory)
    {
        return new SubcategoryResponseDTO
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            CategoryId = subcategory.CategoryId
        };
    }

    public static Subcategory ToSubcategoryModel(SubcategoryCreationDTO dto)
    {
        return new Subcategory
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CategoryId = dto.CategoryId
        };
    }
}