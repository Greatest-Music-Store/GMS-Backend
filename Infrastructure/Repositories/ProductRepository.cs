namespace GMS_Backend.Infrastructure.Repositories;

using Domain.Filters;
using GMS_Backend.Data;
using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Include(p => p.Feedbacks)
            .FirstOrDefaultAsync(
                p => p.ProductId == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Include(p => p.Feedbacks)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(
                p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrWhiteSpace(filter.Brand))
        {
            query = query.Where(
                p => p.Brand.Contains(filter.Brand));
        }

        if (!(filter.MinPrice == null) && !(filter.MaxPrice == null))
        {
            query = query.Where(
                p => p.Price > filter.MinPrice && p.Price < filter.MaxPrice);
        }

        if (filter.CategoryId != null)
        {
            query = query.Where(
                p => p.CategoryId == filter.CategoryId);
        }

        if (filter.SubCategoryId != null)
        {
            query = query.Where(
                p => p.SubcategoryId == filter.SubCategoryId);
        }

        return await query.ToListAsync();
    }
}