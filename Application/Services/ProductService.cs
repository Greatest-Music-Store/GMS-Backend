using GMS_Backend.Domain.Filters;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;

namespace GMS_Backend.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _repository.CreateAsync(product);

        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter)
    {
        return await _repository.GetAllAsync(filter);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null) throw new KeyNotFoundException();

        await _repository.DeleteAsync(product);
    }

    public static double GetAverageRating(Product product)
    {
        if (product.Feedbacks.Count == 0) return 0; 
        
        return Math.Round(product.Feedbacks.Average(f => f.Rating), 1);
    }
}