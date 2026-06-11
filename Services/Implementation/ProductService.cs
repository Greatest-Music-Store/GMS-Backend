using GMS_Backend.Data;
using GMS_Backend.DTOs.Product;
using GMS_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GMS_Backend.Mappers;
using GMS_Backend.Models;

namespace GMS_Backend.Services.Implementation;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ProductResponseDTO> CreateAsync(ProductCreationDTO dto)
    {
        var product = ProductMapper.ToModel(dto);

        _context.Add(product);
        await _context.SaveChangesAsync();

        return ProductMapper.ToDto(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            throw new KeyNotFoundException("Produto não encontrado.");

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync(string? name = null, Guid? categoryId = null, Guid? subCategoryId = null, int? minPrice = null, int? maxPrice = null, string? sortBy = null, string? brand = null)
    {
        var query = _context.Products
        .Include(p => p.Category)
        .Include(p => p.SubCategory)
        .Include(p => p.Feedbacks)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p => p.Brand.Contains(brand));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (subCategoryId.HasValue)
        {
            query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
        }

        if (minPrice.HasValue && maxPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        if (sortBy == "rating_desc")
        {
            query = query.OrderByDescending(p => p.Feedbacks.Any() ? p.Feedbacks.Average(f => f.Rating) : 0);
        }

        if (sortBy == "rating_asc")
        {
            query = query.OrderBy(p => p.Feedbacks.Any() ? p.Feedbacks.Average(f => f.Rating) : 0);
        }

        var products = await query.ToListAsync();

        return products
            .Select(ProductMapper.ToDto)
            .ToList();
    }

    public static double GetAverageRating(Product p)
    {
        if (p.Feedbacks.Count == 0) return 0; 
        
        return Math.Round(p.Feedbacks.Average(f => f.Rating), 1);
    }

    public async Task<ProductResponseDTO?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.SubCategory)
        .Include(p => p.Feedbacks)
        .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
            return null;

        return ProductMapper.ToDto(product);
    }
}