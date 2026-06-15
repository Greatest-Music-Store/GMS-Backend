namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Category Category)
    {
        _context.Categories.Add(Category);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category Category)
    {
        _context.Categories.Remove(Category);

        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
    }
}