namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Data;
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly AppDbContext _context;

    public SubcategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Subcategory subcategory)
    {
        _context.Subcategories.Add(subcategory);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Subcategory subcategory)
    {
        _context.Subcategories.Remove(subcategory);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Subcategories
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Subcategory?> GetByIdAsync(Guid id)
    {
        return await _context.Subcategories
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}