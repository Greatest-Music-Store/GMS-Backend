namespace GMS_Backend.Infrastructure.Repositories;

using FuzzySharp;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;
using GMS_Backend.Infrastructure.Data;
using GMS_Backend.Infrastructure.QueryExtensions;

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
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter, string? search)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Include(p => p.Feedbacks)
            .Apply(filter)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(search))
        {
            return products;
        }
        return products
            .Where(p => Fuzz.PartialRatio(p.Name, search) >= 60)
            .OrderByDescending(p => Fuzz.PartialRatio(p.Name, search))
            .ToList();
    }

    public async Task<IEnumerable<Product>> GetOffers()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Where(p => p.DiscountPercentage > 0)
            .ToListAsync();

        return products;
    }

    public async Task<IEnumerable<Product>> GetRecommended()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Where(p => p.DiscountPercentage == 0)
            .ToListAsync();

        return products;
    }

    public async Task<Product> Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}