namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.Domain.Filters;
using GMS_Backend.Models;

public interface IProductRepository
{
    Task CreateAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync(ProductFilter filter);
    Task DeleteAsync(Product product);
}