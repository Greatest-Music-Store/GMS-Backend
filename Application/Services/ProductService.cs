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

        return await _repository.GetByIdAsync(product.ProductId) ?? throw new Exception("Erro ao carregar produto criado.");
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter, string? search)
    {
        return await _repository.GetAllAsync(filter, search);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null) throw new KeyNotFoundException();

        await _repository.DeleteAsync(product);
    }

    public static double GetAverageRating(Product product)
    {
        if (product.Feedbacks == null || product.Feedbacks.Count == 0)
        return 0;

        return Math.Round(product.Feedbacks.Average(f => f.Rating), 1);
    }
}