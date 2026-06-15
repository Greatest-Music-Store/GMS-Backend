using GMS_Backend.Domain.Models;
namespace GMS_Backend.Domain.Repositories;

public interface ISubcategoryRepository
{
    Task CreateAsync(Subcategory subcategory);
    Task<Subcategory?> GetByIdAsync(Guid id);
    Task<IEnumerable<Subcategory>> GetByCategoryIdAsync(Guid categoryId);
    Task DeleteAsync(Subcategory subcategory);
}