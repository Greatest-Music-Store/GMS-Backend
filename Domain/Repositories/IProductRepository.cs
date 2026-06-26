namespace GMS_Backend.Domain.Repositories;

using GMS_Backend.Domain.Filters;
using GMS_Backend.Domain.Models;

public interface IProductRepository
{
    Task CreateAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter, string? search);
    Task DeleteAsync(Product product);
    Task<IEnumerable<Product>> GetOffers();
    Task<Product> Update(Product product);
}