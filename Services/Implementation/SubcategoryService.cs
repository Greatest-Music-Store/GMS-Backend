using Microsoft.EntityFrameworkCore;
using GMS_Backend.Data;
using GMS_Backend.DTOs.Categories;
using GMS_Backend.Mappers;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Services;

public class SubcategoryService : ISubcategoryService
{
    private readonly AppDbContext _context;

    public SubcategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SubcategoryResponseDTO> CreateAsync(SubcategoriesCreationDTO dto)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            throw new KeyNotFoundException("Categoria não existe");

        var subcategory = CategoriesMapper.ToSubcategoryModel(dto);

        _context.SubCategories.Add(subcategory);
        await _context.SaveChangesAsync();

        return CategoriesMapper.ToSubcategoryDto(subcategory);
    }

    public async Task<SubcategoryResponseDTO?> GetByIdAsync(Guid id)
    {
        var sub = await _context.SubCategories
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null)
            return null;

        return CategoriesMapper.ToSubcategoryDto(sub);
    }

    public async Task<IEnumerable<SubcategoryResponseDTO>> GetByCategoryIdAsync(Guid categoryId)
    {
        var subs = await _context.SubCategories
            .Where(s => s.CategoryId == categoryId)
            .ToListAsync();

        return subs.Select(CategoriesMapper.ToSubcategoryDto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sub = await _context.SubCategories
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null)
            throw new KeyNotFoundException("Subcategoria não encontrada");

        _context.SubCategories.Remove(sub);
        await _context.SaveChangesAsync();
    }
}