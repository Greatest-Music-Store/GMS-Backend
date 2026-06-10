using Microsoft.EntityFrameworkCore;
using GMS_Backend.Data;
using GMS_Backend.DTOs.Categories;
using GMS_Backend.Mappers;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponseDTO> CreateAsync(CategoryCreationDTO dto)
    {
        var category = CategoriesMapper.ToModel(dto);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CategoriesMapper.ToDto(category);
    }

    public async Task<CategoryResponseDTO?> GetByIdAsync(Guid id)
    {
        var category = await _context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return null;

        return CategoriesMapper.ToDto(category);
    }

    public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();

        return categories.Select(CategoriesMapper.ToDto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            throw new KeyNotFoundException("Categoria não encontrada");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}