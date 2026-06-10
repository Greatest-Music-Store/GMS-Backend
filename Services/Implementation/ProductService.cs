using GMS_Backend.Data;
using GMS_Backend.DTOs.Product;
using GMS_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GMS_Backend.Mappers;

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

    public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync()
    {
        var products = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.SubCategory)
        .ToListAsync();

        return products
            .Select(ProductMapper.ToDto)
            .ToList();
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