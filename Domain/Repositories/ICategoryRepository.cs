using GMS_Backend.Domain.Models;
namespace GMS_Backend.Domain.Repositories;

public interface ICategoryRepository
{
    Task CreateAsync(Category category);
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task DeleteAsync(Category category);
}